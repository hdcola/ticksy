using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using ticksy.Dialogs;

namespace ticksy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            // Check xml config
            string connStr = ValidateConfigXml();
            if (connStr == "")
            {
                // No valid config.xml in place to proceed
                Environment.Exit(0);
            }

            // Load Database context into Globals
            try
            {
                Globals.DbContext = new Entities(connStr);
                Trace.WriteLine("Successfully connected to Azure database.");
            }
            catch (SystemException ex)
            {
                MessageBox.Show($"Fatal error\n {ex.Message}", Globals.AppName, MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Environment.Exit(1);
            }

            // Start main application
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private string ValidateConfigXml()
        {
            Dictionary<string, string> configs = null;

            // Get the path to the AppData folder
            string appDataPath = Globals.GetAppDataPath();
            string filePath = Path.Combine(appDataPath, "config.xml");

            // Validate config.xml exists
            if (!File.Exists(filePath))
            {
                if (!TryConfigureDb(out configs)) return "";
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
                    MessageBox.Show($"Unable to read config.xml\n {ex.Message}", Globals.AppName,
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    if (!TryConfigureDb(out configs)) return "";
                }
                catch (SqlException ex)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    MessageBox.Show( $"Unable to connect: {ex.Message}", Globals.AppName, MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    // Prefill dialog with existing values
                    if (!TryConfigureDb(out configs, server, port, username, dbName)) return "";
                }
            }

            return DbConfigDialog.BuildConnectionString(configs);
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
            DbConfigDialog dialog = new DbConfigDialog(server, port, username, dbName);

            if (dialog.ShowDialog() == true)
            {
                configs = dialog.Configs;
                return true;
            }

            return false;
        }
    }
}
