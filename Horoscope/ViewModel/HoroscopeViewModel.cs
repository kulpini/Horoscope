using Horoscope.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Horoscope.ViewModel
{
    public class HoroscopeViewModel : INotifyPropertyChanged
    {
        public Person FirstPerson { get; set; }

        public Person SecondPerson { get; set; }

        public Pair Pair { get; set; }

        private int reportProgress;
        public int ReportProgress
        {
            get => reportProgress;
            set
            {
                reportProgress = value;
                OnPropertyChanged(nameof(ReportProgress));
            }
        }

        public HoroscopeViewModel()
        {
            FirstPerson = new Person(Sex.Male);
            SecondPerson = new Person(Sex.Female);
            Pair = new Pair(FirstPerson, SecondPerson);
        }

        private RelayCommand setFirstPersonGender;
        public RelayCommand SetFirstPersonGender =>
            setFirstPersonGender ??= new RelayCommand(obj =>
              {
                  FirstPerson.Gender = obj.ToString() == "Male" ? Sex.Male : Sex.Female;
              });

        private RelayCommand setFirstPersonLocale;
        public RelayCommand SetFirstPersonLocale =>
            setFirstPersonLocale ??= new RelayCommand(obj =>
             {
                 FirstPerson.Locale = obj.ToString() == "UA" ? "UA" : "RU";
             });

        private RelayCommand calculateHoroscope;
        public RelayCommand CalculateHoroscope =>
            calculateHoroscope ??= new RelayCommand(obj =>
              {
                  ((Person)obj).CalculateHoroscope();
              });

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            ReportProgress = e.ProgressPercentage;
        }

        private RelayCommand personalReport;
        public RelayCommand PersonalReport =>
            personalReport ??= new RelayCommand(obj =>
            {
                ReportProgress = 0;
                BackgroundWorker worker = new();
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.DoWork += MakePersonalReport;
                worker.RunWorkerAsync(obj);
            });

        private RelayCommand pairReport;
        public RelayCommand PairReport =>
            pairReport ??= new RelayCommand(obj =>
            {
                ReportProgress = 0;
                BackgroundWorker worker = new();
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.DoWork += MakePairReport;
                worker.RunWorkerAsync(obj);
            });

        private void MakePersonalReport(object? sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            string path = Properties.Settings.Default.PersonalDescriptionsDir;
            string outputPath = Properties.Settings.Default.OutputFolder;
            PersonalReport report = new((Person)e.Argument, path, outputPath);
            report.MakeReport(sender);
            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            MessageBox.Show($"Файл сохранён! \n {time:mm\\:ss}");
        }

        private void MakePairReport(object? sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            string path = Properties.Settings.Default.PairDescriptionsDir;
            string outputPath = Properties.Settings.Default.OutputFolder;
            PairReport report = new(Pair, path, outputPath);
            report.MakeReport(sender);
            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            MessageBox.Show($"Файл сохранён! \n{time:mm\\:ss}");
        }

        private RelayCommand setSecondPersonGender;
        public RelayCommand SetSecondPersonGender =>
            setSecondPersonGender ??= new RelayCommand(obj =>
            {
                SecondPerson.Gender = obj.ToString() == "Male" ? Sex.Male : Sex.Female;
            });

        private RelayCommand setSecondPersonLocale;
        public RelayCommand SetSecondPersonLocale =>
            setSecondPersonLocale ??= new RelayCommand(obj =>
            {
                SecondPerson.Locale = obj.ToString() == "UA" ? "UA" : "RU";
            });

        private RelayCommand calculatePairHoroscope;
        public RelayCommand CalculatePairHoroscope =>
            calculatePairHoroscope ??= new RelayCommand(obj =>
              {
                  Pair.CalculateHoroscope();
              });

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private RelayCommand setPairLocale;
        public RelayCommand SetPairLocale =>
            setPairLocale ??= new RelayCommand(obj =>
              {
                  Pair.Locale = obj.ToString() == "UA" ? "UA" : "RU";
              });
    }
}
