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

        public MainWindow()
        {
            InitializeComponent();

            txtCount.Text = count.ToString();
        }
        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!ValidateConfigXml(out string connStr))
            {
                // No valid config.xml in place to proceed
                Environment.Exit(0);
            }

            // Load Database context into Globals
            try
            {
                Globals.DbContext = new Entities(connStr);
                Trace.WriteLine("Successfully connected to Azure database.");
                LoginMainDlg dialog = new LoginMainDlg();
                dialog.Owner = this;

                if (dialog.ShowDialog() == true)
                {
                    // Do something with the data
                }
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

        
        
        private bool ValidateConfigXml(out string connStr)
        {
            connStr = null;
            Dictionary<string,string> configs = null;

            // Get the path to the AppData folder
            string appDataPath = Globals.GetAppDataPath();
            string filePath = Path.Combine(appDataPath, "config.xml");

            // Validate config.xml exists
            if (!File.Exists(filePath))
            {
                if (!TryConfigureDb(out configs)) return false;
            }

            // Read from config.xml and validate values are working
            if (configs == null)
            {
                string server = "";
                string port = "";
                string username = "";
                string dbName = "";

                try
                {
                    Dictionary<string, string> fileConfigs = XmlHelper.GetContent(filePath);
                    fileConfigs.TryGetValue("Server", out server);
                    fileConfigs.TryGetValue("Port", out port);
                    fileConfigs.TryGetValue("Username", out username);
                    fileConfigs.TryGetValue("Password", out string password);
                    fileConfigs.TryGetValue("DatabaseName", out dbName);

                    string connTestStr = DbConfigDialog.BuildConnectionString(server, port, username, password, dbName);

                    Mouse.OverrideCursor = Cursors.Wait;
                    DbConfigDialog.DbConnect(connTestStr);
                    Mouse.OverrideCursor = Cursors.Arrow;

                    // Valid file config values
                    configs = fileConfigs;
                }
                catch (Exception ex) when (ex is XmlException || ex is SecurityException)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    MessageBox.Show(this, $"Unable to read config.xml\n {ex.Message}", Globals.AppName,
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    if (!TryConfigureDb(out configs)) return false;
                }
                catch (SqlException ex)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    MessageBox.Show(this, $"Unable to connect: {ex.Message}", Globals.AppName, MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    // Prefill dialog with existing values
                    if (!TryConfigureDb(out configs, server, port, username, dbName)) return false;
                }
            }

            connStr = DbConfigDialog.BuildConnectionString(configs);
            return true;
        }

        /// <summary>
        /// This method calls the DbConfigDialog and returns if it was successful or not.
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="dbName"></param>
        /// <returns>True if successfully setup Database config.xml</returns>
        private bool TryConfigureDb(out Dictionary<string, string> configs, string server = "", string port = "", string username = "", string dbName = "")
        {
            configs = null;
            DbConfigDialog dialog = new DbConfigDialog(server, port, username, dbName)
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                configs = dialog.Configs;
                return true;
            }

            return false;
        }
    }
}
