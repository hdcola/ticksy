using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for DbConfigDialog.xaml
    /// </summary>
    public partial class DbConfigDialog : Window
    {
        private const string DefaultPort = "1433";
        private const string DefaultDbName = "ticksydb";

        public DbConfigDialog(string server = "", string port = "", string username = "", string dbName = "")
        {
            InitializeComponent();

            TbxServer.Text = server;
            TbxPort.Text = port;
            TbxUser.Text = username;
            TbxDbName.Text = dbName;
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (TbxPort.Text == "")
            {
                TbxPort.Text = DefaultPort;
            }
            if (TbxDbName.Text == "")
            {
                TbxDbName.Text = DefaultDbName;
            }
        }

        private void TbxPort_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnTestConn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TbxServer.Text == "")
                {
                    TbxServer.Focus();
                    throw new ArgumentException("Server cannot be empty");

                }
                if (TbxUser.Text == "")
                {
                    TbxUser.Focus();
                    throw new ArgumentException("User name cannot be empty");
                }

                if (TbxPort.Text == "")
                {
                    TbxPort.Text = DefaultPort;
                }
                if (TbxDbName.Text == "")
                {
                    TbxDbName.Text = DefaultDbName;
                }

                if (TestConnection())
                {
                    MessageBox.Show(this, "Test connection succeeded", Globals.AppName, MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, $"Database connection couldn't be tested\n {ex.Message}", Globals.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!TestConnection()) return;

            // Write config.xml
            try
            {
                // Get the path to the AppData folder 
                string appDataPath = Globals.GetAppDataPath();
                string filePath = Path.Combine(appDataPath, "config.xml");

                // Create the XML content
                Configs = new Dictionary<string, string>
                {
                    ["Server"] = TbxServer.Text.Trim(),
                    ["Port"] = TbxPort.Text.Trim(),
                    ["Username"] = TbxUser.Text.Trim(),
                    ["Password"] = TbxPass.Password.Trim(),
                    ["DatabaseName"] = TbxDbName.Text.Trim(),
                };

                XmlHelper.Create(filePath, Configs);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Unable to create config.xml\n {ex.Message}", Globals.AppName,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Dictionary<string, string> Configs { get; set; }


        private bool TestConnection()
        {
            string connStr = BuildConnectionString(TbxServer.Text.Trim(), TbxPort.Text.Trim(), TbxUser.Text.Trim(), TbxPass.Password.Trim(), TbxDbName.Text.Trim());

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                DbConnect(connStr);
                Mouse.OverrideCursor = Cursors.Arrow;
                return true;
            }
            catch (SqlException ex)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show(this, $"Test connection failed: {ex.Message}", Globals.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static void DbConnect(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
            }
        }

        public static string BuildConnectionString(string server, string port, string username, string password, string dbName = "", int connectionTimeout = 30)
        {
            return $@"Server={server},{port};User Id={username};Password={password};Initial Catalog={dbName};Encrypt=True;Connection Timeout={connectionTimeout};";
        }

        public static string BuildConnectionString(Dictionary<string, string> configs, int connectionTimeout = 30)
        {
            configs.TryGetValue("Server", out string server);
            configs.TryGetValue("Port", out string port);
            configs.TryGetValue("Username", out string username);
            configs.TryGetValue("Password", out string password);
            configs.TryGetValue("DatabaseName", out string dbName);

            return BuildConnectionString(server, port, username, password, dbName, connectionTimeout);
        }
    }
}
