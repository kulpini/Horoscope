using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Horoscope.Model
{
    public class Point : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string energies;
        public string Energies
        {
            get => energies;
            set
            {
                energies = value;
                OnPropertyChanged(nameof(Energies));
            }
        }

        public Point(string name)
        {
            Name = name;
            Energies = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
