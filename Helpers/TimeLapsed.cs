using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace ticksy.Helpers
{
    public class TimeLapsed : INotifyPropertyChanged
    {
        // Goes with INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TimeSpan _offset;
        private string _currentTime;
        public string CurrentTime { get; set; }
        private Stopwatch Stopwatch { get; set; }
        private Timer _timer;

        public TimeLapsed(TimeSpan? offset = null)
        {
            CurrentTime = TimeSpan.Zero.ToString();
            _offset = offset == null ? TimeSpan.Zero : (TimeSpan)offset;
            Stopwatch = new Stopwatch();
        }

        public void Start()
        {
            Stopwatch.Start();

            // Start a callback every 1s
            _timer = new Timer(
                new TimerCallback((s) => OnPropertyChanged(this, new PropertyChangedEventArgs("CurrentTime"))),
                null, 1000, 1000);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                TimeSpan timespan = Stopwatch.Elapsed + _offset;
                CurrentTime = timespan.ToString(@"hh\:mm\:ss");
                PropertyChanged(sender, e);
            }
        }

        public TimeSpan Stop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
            if (Stopwatch.IsRunning)
            {
                Stopwatch.Stop();
            }
            return Stopwatch.Elapsed + _offset;
        }
    }
}
