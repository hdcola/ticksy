using System;
using System.Windows;
using ticksy.Helpers;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for FocusTimerDlg.xaml
    /// </summary>
    public partial class FocusTimerDlg : Window
    {
        private DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public TimeLapsed TimeLapsed { get; set; }

        public FocusTimerDlg(string entryName, DateTime startDateTime)
        {
            this.Topmost = true;

            StartDateTime = startDateTime;
            TimeLapsed = new TimeLapsed(DateTime.Now - startDateTime);

            InitializeComponent();
            TbEntry.Text = entryName;
            TimeLapsed.Start();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.WorkArea.Width - Width - 2;
            Top = SystemParameters.WorkArea.Height - Height - 2;
        }

        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            TimeSpan timeLapsed = TimeLapsed.Stop();
            EndDateTime = StartDateTime + timeLapsed;

            // Dismiss the dialog
            this.DialogResult = true;
        }
    }
}
