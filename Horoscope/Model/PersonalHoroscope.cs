using Horoscope.Model;
using System;
using System.ComponentModel;
using System.IO;

namespace Horoscope
{
    public class PersonalHoroscope : Horoscopes,INotifyPropertyChanged
    {
        public PersonalHoroscope():base()
        {
            Numbers = new int[27];
            string pointNamesFile = AppDomain.CurrentDomain.BaseDirectory + "PersonalPoints.txt";
            if (File.Exists(pointNamesFile))
                ReadPointsFromFile(pointNamesFile);
            else
                SetDefaultPoints();
        }
        private void SetDefaultPoints()
        {
            Points.Add(new Point("Визитная карточка"));
            Points.Add(new Point("Связь с Ангелами-Хранителями"));
            Points.Add(new Point("То, что принесет блага земной жизни"));
            Points.Add(new Point("Кармический хвост"));
            Points.Add(new Point("Зона комфорта"));
            Points.Add(new Point("Влияние рода отца"));
            Points.Add(new Point("Влияние рода матери"));
            Points.Add(new Point("Карма не отработанная мужчинами рода"));
            Points.Add(new Point("Карма не отработанная женщинами рода"));
            Points.Add(new Point("Линия неба"));
            Points.Add(new Point("Линия земли"));
            Points.Add(new Point("Первое личное предназначение"));
            Points.Add(new Point("Второе предназначение"));
            Points.Add(new Point("Третье предназначение"));
            Points.Add(new Point("Таланты"));
            Points.Add(new Point("Отношения родители-дети"));
            Points.Add(new Point("Отношения с противоположным полом"));
            Points.Add(new Point("Финансы"));
            Points.Add(new Point("Здоровье тела по фиолетовой чакре"));
            Points.Add(new Point("Здоровье тела по синей чакре"));
            Points.Add(new Point("Здоровье тела по голубой чакре"));
            Points.Add(new Point("Здоровье тела по зеленой чакре"));
            Points.Add(new Point("Здоровье тела по желтой чакре"));
            Points.Add(new Point("Здоровье тела по оранжевой чакре"));
            Points.Add(new Point("Здоровье тела по красной чакре"));
        }

        public void Calculate(DateTime birthDate)
        {
            int birthDay = birthDate.Day;
            int birthMonth = birthDate.Month;
            int birthYear = birthDate.Year;
            Numbers[0] = SetNumberValue(birthDay);  // 1-st number
            Numbers[1] = birthMonth;                // 2-nd number
            Numbers[2] = SetNumberValue(DigitsSum(birthYear));      // 3-rd number
            Numbers[3] = SetNumberValue(Numbers[0], Numbers[1], Numbers[2]);    // 4-th number
            Numbers[4] = SetNumberValue(Numbers[0], Numbers[1], Numbers[2], Numbers[3]);
            Numbers[5] = SetNumberValue(Numbers[0], Numbers[1]);
            Numbers[6] = SetNumberValue(Numbers[1], Numbers[2]);
            Numbers[7] = SetNumberValue(Numbers[2], Numbers[3]);
            Numbers[8] = SetNumberValue(Numbers[0], Numbers[3]);
            Numbers[9] = SetNumberValue(Numbers[4], Numbers[2]);
            Numbers[10] = SetNumberValue(Numbers[4], Numbers[3]);
            Numbers[11] = SetNumberValue(Numbers[10], Numbers[9]);
            Numbers[12] = SetNumberValue(Numbers[9], Numbers[11]);
            Numbers[13] = SetNumberValue(Numbers[10], Numbers[11]);
            Numbers[14] = SetNumberValue(Numbers[10], Numbers[3]);
            Numbers[15] = SetNumberValue(Numbers[0], Numbers[4]);
            Numbers[16] = SetNumberValue(Numbers[0], Numbers[15]);
            Numbers[17] = SetNumberValue(Numbers[1], Numbers[3]);
            Numbers[18] = SetNumberValue(Numbers[0], Numbers[2]);
            Numbers[19] = SetNumberValue(Numbers[17], Numbers[18]);
            Numbers[20] = SetNumberValue(Numbers[5], Numbers[7]);
            Numbers[21] = SetNumberValue(Numbers[8], Numbers[6]);
            Numbers[22] = SetNumberValue(Numbers[20], Numbers[21]);
            Numbers[23] = SetNumberValue(Numbers[19], Numbers[22]);
            Numbers[24] = SetNumberValue(Numbers[1], Numbers[4]);
            Numbers[25] = SetNumberValue(Numbers[15], Numbers[4]);
            Numbers[26] = SetNumberValue(Numbers[4], Numbers[2]);
            SetPointValues();
        }

        private void SetPointValues()
        {
            Points[0].Energies = $"{Numbers[0]}";
            Points[1].Energies = $"{Numbers[1]}";
            Points[2].Energies = $"{Numbers[2]}";
            Points[3].Energies = $"{Numbers[3]}, {Numbers[14]}, {Numbers[10]}";
            Points[4].Energies = $"{Numbers[4]}";
            Points[5].Energies = $"{Numbers[20]}";
            Points[6].Energies = $"{Numbers[21]}";
            Points[7].Energies = $"{Numbers[5]}, {Numbers[6]}";
            Points[8].Energies = $"{Numbers[8]}, {Numbers[7]}";
            Points[9].Energies = $"{Numbers[17]}";
            Points[10].Energies = $"{Numbers[18]}";
            Points[11].Energies = $"{Numbers[19]}";
            Points[12].Energies = $"{Numbers[22]}";
            Points[13].Energies = $"{Numbers[23]}";
            Points[14].Energies = $"{Numbers[24]}";
            Points[15].Energies = $"{Numbers[0]}, {Numbers[16]}, {Numbers[15]}";
            Points[16].Energies = $"{Numbers[11]}, {Numbers[13]}, {Numbers[10]}";
            Points[17].Energies = $"{Numbers[9]}, {Numbers[12]}, {Numbers[11]}";
            Points[18].Energies = $"{Numbers[0]}";
            Points[19].Energies = $"{Numbers[16]}";
            Points[20].Energies = $"{Numbers[15]}";
            Points[21].Energies = $"{Numbers[25]}";
            Points[22].Energies = $"{Numbers[4]}";
            Points[23].Energies = $"{Numbers[26]}";
            Points[24].Energies = $"{Numbers[2]}";
        }
    }
}
