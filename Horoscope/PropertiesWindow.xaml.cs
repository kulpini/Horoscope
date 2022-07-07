using Horoscope.ViewModel;
using System.Windows;

namespace Horoscope
{
    /// <summary>
    /// Interaction logic for PropertiesWindow.xaml
    /// </summary>
    public partial class PropertiesWindow : Window
    {
        private PropertiesViewModel viewModel;
        public PropertiesWindow()
        {
            InitializeComponent();
            viewModel = new PropertiesViewModel();
            DataContext = viewModel;
        }
    }
}
