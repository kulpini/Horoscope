using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Horoscope.Model
{
    public class Person : INotifyPropertyChanged
    {
        public DateTime BirthDate { get; set; }
        public Sex Gender { get; set; }
        public string Locale { get; set; }

        private PersonalHoroscope horoscope;
        public PersonalHoroscope Horoscope
        {
            get => horoscope;
            set
            {
                horoscope = value;
                OnPropertyChanged(nameof(Horoscope));
            }
        }

        public Person(Sex gender)
        {
            Gender = gender;
            BirthDate = DateTime.Now;
            Horoscope = new PersonalHoroscope();
            Locale = "RU";
        }

        public void CalculateHoroscope()
        {
            Horoscope.Calculate(BirthDate);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
