using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horoscope.Model
{
    public class PairReport
    {
        const string TEMPATE_FILENAME = "template.docx";
        private string WorkingFolder { get; set; }
        private string OutputFolder { get; set; }
        private readonly Pair Pair;

        public PairReport(Pair pair, string workingFolder, string outputFolder)
        {
            Pair = pair;
            WorkingFolder = Path.Combine(workingFolder, Pair.Locale);
            OutputFolder = outputFolder;
        }

        public void MakeReport(object sender)
        {
            Document document = null;
            string templateFile = $"{WorkingFolder}\\{TEMPATE_FILENAME}";
            try
            {
                Application wordApp = new();
                document = wordApp.Documents.Add(templateFile);
                string prefix = Pair.Locale == "RU" ? "и" : "та";
                string pairBirths = $"{Pair.FirstPerson.BirthDate:d} {prefix} {Pair.SecondPerson.BirthDate:d}";
            document.Bookmarks["Date"].Range.Text = pairBirths;
                for (int i = 1; i <= Pair.Horoscope.Points.Count; i++)
                {
                    ((BackgroundWorker)sender).ReportProgress(i * 100 / Pair.Horoscope.Points.Count);
                    WriteEnergyDescription(wordApp, document, i);
                }
                string filename = Pair.Locale == "RU" ? "Анализ отношений" : "Аналіз стосунків";
                
                string outputFile = $"{OutputFolder}\\{filename} {pairBirths}.docx";
                document.SaveAs2(outputFile);
            }
            finally
            {
                document.Close();
            }
        }

        private void WriteEnergyDescription(Application app, Document document, int item)
        {
            string[] energies = Pair.Horoscope.Points[item - 1].Energies.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
            int i = 0;
            Document energyDocument = null;
            foreach (string energy in energies)
            {
                i++;
                string energyFile = $"{WorkingFolder}\\energy {energy}.docx"; ;
                try
                {
                    energyDocument = app.Documents.Open(energyFile);
                    object beginText = energyDocument.Content.Start;
                    object endText = energyDocument.Content.End;
                    Microsoft.Office.Interop.Word.Range contentRange = energyDocument.Range(ref beginText, ref endText);
                    contentRange.Copy();
                    if (i == 1)
                    {
                        document.Bookmarks[$"Item{item}"].Range.Select();
                    }
                    else
                    {
                        object endRange = document.Bookmarks[$"Item{item}"].Range.End;
                        document.Range(ref endRange, ref endRange).Select();
                    }
                    app.Selection.Paste();
                }
                finally
                {
                    energyDocument.Close();
                }
            }
        }
    }
}
