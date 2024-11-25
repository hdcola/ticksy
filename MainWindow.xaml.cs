using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using ticksy.Dialogs;

namespace ticksy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count = 0;
        private string connStr;

        public MainWindow(string connStr)
        {
            this.connStr = connStr;
            InitializeComponent();

            txtCount.Text = count.ToString();
        }
        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Load Database context into Globals
            try
            {
                Globals.DbContext = new Entities(connStr);
                Trace.WriteLine("Successfully connected to Azure database.");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, $"Fatal error\n {ex.Message}", Globals.AppName, MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            count++;
            txtCount.Text = count.ToString();
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


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginMainDlg dialog = new LoginMainDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
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
    }
}
