using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ticksy.Dialogs;
using ticksy.Helpers;
using ticksy.ViewModels;

namespace ticksy.Views
{
    /// <summary>
    /// Interaction logic for TimeEntriesView.xaml
    /// </summary>
    public partial class TimeEntriesView : UserControl
    {
        private Window Owner { get; }
        private bool IsEntryStarted = false;
        private DateTime StartDateTime { get; set; }
        private DateTime EndDateTime { get; set; }
        public TimeLapsed TimeLapsed { get; set; }
        private User User { get; }
        private TimeEntry CurrentEntry { get; set; }

        public TimeEntriesView(User user, Window owner)
        {
            Owner = owner;
            User = user;
            TimeLapsed = new TimeLapsed();

            InitializeComponent();
            DataContext = new TimeEntriesViewModel(user);
        }

        private void BtnStartEntryTime_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsEntryStarted)
            {
                // Get end date time from TbTimer
                StopEntry();
            }
            else
            {
                // Validation
                if (IsValidEntry())
                {
                    StartEntry();
                }
            }
        }

        private void BtnFocusTimer_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Replace startDateTime with actual entry time start time
            FocusTimerDlg dialog = new FocusTimerDlg("Entry test task name", StartDateTime);
            dialog.Owner = Owner;

            Owner.WindowState = WindowState.Minimized;

            if (dialog.ShowDialog() == true)
            {
                if (dialog.EndDateTime != null)
                {
                    StopEntry();
                }
            }

            Owner.WindowState = WindowState.Normal;
        }

        // Focus on grid when clicking on nothing
        private void TimeEntriesViewUC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Focus();
        }

        private bool IsValidEntry()
        {
            if (!Validator.ValidateInput(TbxName.Text, "Time Entry", 1, 255, out string errorMsg))
            {
                TbErrorName.Text = errorMsg;
                return false;
            }
            if (CmbSelectTask.SelectedIndex < 0)
            {
                TbErrorSelectTask.Text = "Please select a task.";
                return false;
            }
            return true;
        }

        private void StartEntry()
        {
            IsEntryStarted = true;
            StartDateTime = DateTime.Now;

            // Start the timer
            CurrentEntry = new TimeEntry
            {
                Name = TbxName.Text,
                User = User,
                Task = (Task)CmbSelectTask.SelectedItem,
                StartTime = StartDateTime,
                EndTime = DateTime.Now,
                CreatedAt = DateTime.Now,
            };
            try
            {
                Globals.DbContext.Set<TimeEntry>().Add(CurrentEntry);
                Globals.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Start the timer
            TimeLapsed = new TimeLapsed();
            TbTimer.GetBindingExpression(TextBlock.TextProperty).UpdateTarget(); // Update the binding...
            TimeLapsed.Start();

            // Change label to stop
            BtnStartEntryTime.Content = "STOP";

            // Enable the Magical Floating Timer
            BtnFocusTimer.IsEnabled = true;
        }

        private void StopEntry()
        {
            TimeSpan timeLapsed = TimeLapsed.Stop();
            EndDateTime = StartDateTime + timeLapsed;
            Trace.WriteLine($"End time entry: {EndDateTime}");

            // Save database entry
            try
            {
                CurrentEntry.EndTime = EndDateTime;
                Globals.DbContext.SaveChanges();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }

            IsEntryStarted = false;
            BtnStartEntryTime.Content = "START";
            BtnFocusTimer.IsEnabled = false;
        }

        private void TbxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorName.Text = "";
        }

        private void CmbSelectTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbErrorSelectTask.Text = "";
        }
    }
}
