using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ticksy.Dialogs;
using ticksy.ViewModels;

namespace ticksy.Views
{
    /// <summary>
    /// Interaction logic for TimeEntriesView.xaml
    /// </summary>
    public partial class TimeEntriesView : UserControl
    {
        private Window Owner { get; set; }
        public TimeEntriesView(User user, Window owner)
        {
            Owner = owner;

            InitializeComponent();
            DataContext = new TimeEntriesViewModel(user);
        }

        private void BtnStartEntryTime_OnClick(object sender, RoutedEventArgs e)
        {
            // Create the database entry, with start datetime now

            // Start the timer

        }

        private void BtnFocusTimer_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Replace startDateTime with actual entry time start time
            DateTime startDateTime = DateTime.Now.AddMinutes(-2);
            FocusTimerDlg dialog = new FocusTimerDlg("Entry test task name", startDateTime);
            dialog.Owner = Owner;

            Owner.WindowState = WindowState.Minimized;

            if (dialog.ShowDialog() == true)
            {
                DateTime endDateTime = dialog.EndDateTime;
                Trace.WriteLine($"End time entry: {endDateTime}");

                // TODO save the endDateTime to database
            }

            Owner.WindowState = WindowState.Normal;
        }
    }
}
