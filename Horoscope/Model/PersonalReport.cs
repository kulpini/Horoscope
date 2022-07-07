using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace Horoscope.Model
{
    public class PersonalReport
    {
        const string TEMPLATE_FILENAME = "template.docx";
        private string WorkingFolder { get; set; }
        private string OutputFolder { get; set; }
        private Person Person { get; set; }
        public PersonalReport(Person person, string descriptionsFolder, string outputFolder)
        {
            Person = person;
            WorkingFolder = Path.Combine(descriptionsFolder, Person.Locale);
            OutputFolder = outputFolder;
        }

        public void MakeReport(object sender)
        {
            Document document = null;
            string templateFile = $"{WorkingFolder}\\{TEMPLATE_FILENAME}";
            try
            {
                Application wordApp = new();
                document = wordApp.Documents.Add(templateFile);
                document.Bookmarks["Date"].Range.Text = Person.BirthDate.ToString("d");
                for (int i = 1; i <= Person.Horoscope.Points.Count; i++)
                {
                    ((BackgroundWorker)sender).ReportProgress(i * 100 / (Person.Horoscope.Points.Count + 1));
                    WriteEnergyDescription(wordApp, document, i);
                }
                AddPointTableToReport(document);
                ((BackgroundWorker)sender).ReportProgress(100);
                string filename = Person.Locale == "RU" ? "Анализ" : "Аналіз";
                string outputFile = $"{OutputFolder}\\{filename} {Person.BirthDate:d}.docx";
                document.SaveAs2(outputFile);
            }
            finally
            {
                document.Close();
            }
        }

        private void WriteEnergyDescription(Application app, Document document, int item)
        {
            string[] energies = Person.Horoscope.Points[item - 1].Energies.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
            int i = 0;
            Document energyDocument = null;
            foreach (string energy in energies)
            {
                i++;
                string energyFile = GetFileName(energy);
                try
                {
                    energyDocument = app.Documents.Open(energyFile);
                    int rowCount = energyDocument.Tables[1].Rows.Count;
                    if (item == 18) rowCount++;
                    object beginCell = energyDocument.Tables[1].Cell(1, 1).Range.Start;
                    object endCell = energyDocument.Tables[1].Cell(rowCount - 1, 1).Range.End;
                    Microsoft.Office.Interop.Word.Range cellRange = energyDocument.Range(ref beginCell, ref endCell);
                    cellRange.Copy();
                    if (i == 1)
                        document.Bookmarks[$"Item{item}"].Range.Select();
                    else
                    {
                        int n = document.Tables.Count;
                        object beginRange = document.Tables[n].Range.End;
                        object endRange = document.Tables[n].Range.End;
                        document.Range(ref beginRange, ref endRange).Select();
                    }
                    app.Selection.Paste();
                }
                finally
                {
                    energyDocument.Close();
                }
            }
        }

        private string GetFileName(string energy)
        {
            string fileIndex = energy;
            if (int.Parse(energy) == 3 || int.Parse(energy) == 4)
                fileIndex = $"{fileIndex} {Person.Gender}";
            return $"{WorkingFolder}\\energy {fileIndex}.docx";
        }

        private void AddPointTableToReport(Document document)
        {
            var paragraph = document.Paragraphs.Add();
            paragraph.Range.Text = "\n\n\n";
            var tableRange = paragraph.Range;
            string filename = Path.Combine(WorkingFolder, "PointsTable.txt");
            Table pointTable;
            if (File.Exists(filename))
            {
                string[] points = File.ReadAllLines(filename);
                pointTable = document.Tables.Add(tableRange, points.Length, 2);
                for (int i = 0; i < points.Length; i++)
                {
                    pointTable.Cell(i + 1, 1).Range.Text = points[i];
                    pointTable.Cell(i + 1, 1).Range.Font.Bold = 0;
                }
            }
            else
            {
                pointTable = document.Tables.Add(tableRange, Person.Horoscope.Points.Count, 2);
                for (int i = 0; i < Person.Horoscope.Points.Count; i++)
                {
                    pointTable.Cell(i + 1, 1).Range.Text = Person.Horoscope.Points[i].Name;
                    pointTable.Cell(i + 1, 1).Range.Font.Bold = 0;
                }
            }
            pointTable.Borders.Enable = 1;
            pointTable.Columns[1].SetWidth(300, WdRulerStyle.wdAdjustProportional);
            for (int i = 0; i < Person.Horoscope.Points.Count; i++)
            {
                pointTable.Cell(i + 1, 2).Range.Text = Person.Horoscope.Points[i].Energies;
                pointTable.Cell(i + 1, 2).Range.Font.Bold = 0;
            }
        }
    }
}
