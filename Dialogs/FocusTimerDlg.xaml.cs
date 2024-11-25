using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for FocusTimerDlg.xaml
    /// </summary>
    public partial class FocusTimerDlg : Window, INotifyPropertyChanged
    {
        // Goes with INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Timer _timer;
        private readonly TimeSpan _offset;
        private Stopwatch Stopwatch { get; set; }
        private DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string CurrentTimespan { get; set; }

        public FocusTimerDlg(string entryName, DateTime startDateTime)
        {
            StartDateTime = startDateTime;
            _offset = DateTime.Now - startDateTime;

            this.Stopwatch = new Stopwatch();
            this.Stopwatch.Start();

            // Start a callback every 1s
            this._timer = new Timer(
                new TimerCallback((s) => OnPropertyChanged(this, new PropertyChangedEventArgs("CurrentTimespan"))),
                null, 1000, 1000);

            InitializeComponent();
            TbEntry.Text = entryName;
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.WorkArea.Width - Width - 2;
            Top = SystemParameters.WorkArea.Height - Height - 2;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                TimeSpan timespan = Stopwatch.Elapsed + _offset;
                CurrentTimespan = timespan.ToString(@"hh\:mm\:ss");

                PropertyChanged(sender, e);
            }
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            if (Stopwatch.IsRunning)
            {
                Stopwatch.Stop();
                _timer.Dispose();

                EndDateTime = StartDateTime + Stopwatch.Elapsed + _offset;
                // Dismiss the dialog
                this.DialogResult = true;
            }
                
        }
    }
}
