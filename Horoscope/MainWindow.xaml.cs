using Horoscope.ViewModel;
using System.Windows;

namespace Horoscope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HoroscopeViewModel horoscopeViewModel;
        public MainWindow()
        {
            InitializeComponent();
            horoscopeViewModel = new HoroscopeViewModel();
            DataContext = horoscopeViewModel;
        }

        private void PropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            PropertiesWindow propertiesWindow = new();
            propertiesWindow.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
