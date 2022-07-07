using Ookii.Dialogs.Wpf;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Horoscope.ViewModel
{
    public class PropertiesViewModel : INotifyPropertyChanged
    {
        private string personalFolder;
        public string PersonalFolder
        {
            get => personalFolder;
            set
            {
                personalFolder = value;
                Properties.Settings.Default.PersonalDescriptionsDir = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(PersonalFolder));
            }
        }

        private string pairFolder;
        public string PairFolder
        {
            get => pairFolder;
            set
            {
                pairFolder = value;
                Properties.Settings.Default.PairDescriptionsDir = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(PairFolder));
            }
        }
        private string outputFolder;
        public string OutputFolder
        {
            get => outputFolder;
            set
            {
                outputFolder = value;
                Properties.Settings.Default.OutputFolder = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(OutputFolder));
            }
        }

        private static bool IsDirCorrect(string path)
        {
            string[] subDirs = Directory.GetDirectories(path);
            return (subDirs.Contains($"{path}\\RU") && subDirs.Contains($"{path}\\UA"));
        }

        public PropertiesViewModel()
        {
            string personalFolder = Properties.Settings.Default.PersonalDescriptionsDir;
            PersonalFolder = personalFolder == "" ? AppDomain.CurrentDomain.BaseDirectory : personalFolder;
            string pairFolder = Properties.Settings.Default.PairDescriptionsDir;
            PairFolder = pairFolder == "" ? AppDomain.CurrentDomain.BaseDirectory : pairFolder;
            string outputFolder = Properties.Settings.Default.OutputFolder;
            OutputFolder = outputFolder == "" ? AppDomain.CurrentDomain.BaseDirectory : outputFolder;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand setPersonalFolder;
        public RelayCommand SetPersonalFolder =>
            setPersonalFolder ??= new RelayCommand(obj =>
                    {
                        VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog();
                        if (PersonalFolder != "" && new DirectoryInfo(PersonalFolder).Exists)
                            folderDialog.SelectedPath = PersonalFolder + "\\";
                        else
                            folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                        if (folderDialog.ShowDialog() == true)
                        {
                            string path = folderDialog.SelectedPath;
                            if (IsDirCorrect(path))
                                PersonalFolder = path;
                            else
                                MessageBox.Show("Выбранная папка не содержит языковые локализации описаний энергий!");
                        }

                    });
        private RelayCommand setPairFolder;
        public RelayCommand SetPairFolder =>
            setPairFolder ??= new RelayCommand(obj =>
               {
                   VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog();
                   if (PairFolder != "" && new DirectoryInfo(PairFolder).Exists)
                       folderDialog.SelectedPath = PairFolder + "\\";
                   else
                       folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                   if (folderDialog.ShowDialog() == true)
                   {
                       string path = folderDialog.SelectedPath;
                       if (IsDirCorrect(path))
                           PairFolder = path;
                       else
                           MessageBox.Show("Выбранная папка не содержит языковые локализации описаний энергий!");
                   }
               });

        private RelayCommand setOutputFolder;
        public RelayCommand SetOutputFolder =>
            setOutputFolder ??= new RelayCommand(obj =>
              {
                  VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog();
                  if (PairFolder != "" && new DirectoryInfo(PairFolder).Exists)
                      folderDialog.SelectedPath = PairFolder + "\\";
                  else
                      folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                  if (folderDialog.ShowDialog() == true)
                      OutputFolder = folderDialog.SelectedPath;
              });
    }
}
