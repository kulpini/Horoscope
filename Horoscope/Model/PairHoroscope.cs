using System;
using System.ComponentModel;
using System.IO;

namespace Horoscope.Model
{
    public class PairHoroscope : Horoscopes, INotifyPropertyChanged
    {
        public PairHoroscope()
        {
            Numbers = new int[24];
            string pointNamesFile = AppDomain.CurrentDomain.BaseDirectory + "PairPoints.txt";
            if (File.Exists(pointNamesFile))
                ReadPointsFromFile(pointNamesFile);
            else
                SetDefaultPoints();
        }

        private void SetDefaultPoints()
        {
            Points.Add(new Point("Общие задачи"));
            Points.Add(new Point("Зона комфорта"));
            Points.Add(new Point("Предназначение"));
            Points.Add(new Point("Предназначение 2"));
            Points.Add(new Point("Линия неба"));
            Points.Add(new Point("Линия земли"));
            Points.Add(new Point("Гармонизация отношений"));
        }

        public void Calculate(PersonalHoroscope firstHoroscope,PersonalHoroscope secondHoroscope)
        {
            Numbers[0] = SetNumberValue(firstHoroscope.Numbers[0],secondHoroscope.Numbers[0]);  // 1-st number
            Numbers[1] = SetNumberValue(firstHoroscope.Numbers[1], secondHoroscope.Numbers[1]);  
            Numbers[2] = SetNumberValue(firstHoroscope.Numbers[2],secondHoroscope.Numbers[2]);
            Numbers[3] = SetNumberValue(firstHoroscope.Numbers[3], secondHoroscope.Numbers[3]);
            Numbers[4] = SetNumberValue(firstHoroscope.Numbers[4], secondHoroscope.Numbers[4]);
            Numbers[5] = SetNumberValue(firstHoroscope.Numbers[5], secondHoroscope.Numbers[5]);
            Numbers[6] = SetNumberValue(firstHoroscope.Numbers[6], secondHoroscope.Numbers[6]);
            Numbers[7] = SetNumberValue(firstHoroscope.Numbers[7], secondHoroscope.Numbers[7]);
            Numbers[8] = SetNumberValue(firstHoroscope.Numbers[8], secondHoroscope.Numbers[8]);
            Numbers[9] = SetNumberValue(Numbers[4], Numbers[2]);
            Numbers[10] = SetNumberValue(Numbers[4], Numbers[3]);
            Numbers[11] = SetNumberValue(Numbers[9], Numbers[10]);
            
            Numbers[13] = SetNumberValue(Numbers[10], Numbers[11]);

            Numbers[17] = SetNumberValue(Numbers[1], Numbers[3]);
            Numbers[18] = SetNumberValue(Numbers[0], Numbers[2]);
            Numbers[19] = SetNumberValue(Numbers[17], Numbers[18]);
            Numbers[20] = SetNumberValue(Numbers[5], Numbers[7]);
            Numbers[21] = SetNumberValue(Numbers[8], Numbers[6]);
            Numbers[22] = SetNumberValue(Numbers[20], Numbers[21]);
            Numbers[23] = SetNumberValue(Numbers[19], Numbers[22]);
            SetPointValues();
        }

        private void SetPointValues()
        {
            Points[0].Energies = $"{Numbers[0]}, {Numbers[19]}";
            Points[1].Energies = $"{Numbers[4]}";
            Points[2].Energies = $"{Numbers[23]}";
            Points[3].Energies = $"{Numbers[22]}";
            Points[4].Energies = $"{Numbers[17]}";
            Points[5].Energies = $"{Numbers[18]}, {Numbers[2]}";
            Points[6].Energies = $"{Numbers[11]}, {Numbers[13]}, {Numbers[10]}";
        }

    }
}
