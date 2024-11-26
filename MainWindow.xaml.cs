using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Xml;
using ticksy.Dialogs;

namespace ticksy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = Globals.AppName;
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectDlg dialog = new CreateProjectDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            CreateInvoiceDlg dialog = new CreateInvoiceDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginMainDlg dialog = new LoginMainDlg();
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterDlg dialog = new RegisterDlg();
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutDlg dialog = new AboutDlg() { Owner = this };
            dialog.ShowDialog();
        }

        /*
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginMainDlg dialog = new LoginMainDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }*/
        private void MIFocusTimer_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Replace startDateTime with actual entry time start time
            DateTime startDateTime = DateTime.Now.AddMinutes(-2);
            FocusTimerDlg dialog = new FocusTimerDlg("Entry test task name",  startDateTime);
            dialog.Owner = this;

            this.WindowState = WindowState.Minimized;

            if (dialog.ShowDialog() == true)
            {
                DateTime endDateTime = dialog.EndDateTime;
                Trace.WriteLine($"End time entry: {endDateTime}");

                // TODO save the endDateTime to database
            }

            this.WindowState = WindowState.Normal;
        }

        private void MIDashboard_OnClick(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Owner = this;

            if (dashboard.ShowDialog() == true)
            {
                // Do stuff
            }
        }
    }
}
