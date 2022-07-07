using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Horoscope.Model
{
    public class Horoscopes:INotifyPropertyChanged
    {
        const int GOLDEN_VALUE = 22;
        private ObservableCollection<Point> points;
        public ObservableCollection<Point> Points
        {
            get => points;
            set
            {
                points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        internal int[] Numbers;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Horoscopes()
        {
            Points = new ObservableCollection<Point>();
        }
        private protected static int DigitsSum(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        private protected static int SetNumberValue(params int[] numbers)
        {
            int sum = numbers.Sum();
            if (sum <= GOLDEN_VALUE)
                return sum;
            else
                return DigitsSum(sum);
        }
        private protected void ReadPointsFromFile(string filename)
        {
            string[] pointNames = File.ReadAllLines(filename);
            foreach (string name in pointNames)
            {
                Points.Add(new Point(name));
            }
        }
    }
}
