using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Windows.Annotations.Storage;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for DbConfigDialog.xaml
    /// </summary>
    public partial class DbConfigDialog : Window
    {
        private const string DefaultPort = "1433";
        private const string DefaultDbName = "ticksydb";
        public DbConfigDialog()
        {
            InitializeComponent();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            TbxPort.Text = DefaultPort;
            TbxDbName.Text = DefaultDbName;
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
                MessageBox.Show(this, $"Database connection couldn't be tested: {ex.Message}", Globals.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
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

                // Define the file path
                string filePath = Path.Combine(appDataPath, "config.xml");

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Create the XML content
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Settings");

                    writer.WriteElementString("Server", TbxServer.Text.Trim());
                    writer.WriteElementString("Port", TbxPort.Text.Trim());
                    writer.WriteElementString("Username", TbxUser.Text.Trim());
                    writer.WriteElementString("Password", TbxPass.Password.Trim());
                    writer.WriteElementString("DatabaseName", TbxDbName.Text.Trim());

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Unable to create config.xml: {ex.Message}", Globals.AppName,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool TestConnection()
        {
            string connStr = $@"Server={TbxServer.Text.Trim()},{TbxPort.Text.Trim()};Initial Catalog={TbxDbName.Text.Trim()};User Id={TbxUser.Text.Trim()};Password={TbxPass.Password.Trim()};Encrypt=True;Connection Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    conn.Open();

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                    return true;
                }
                catch (SqlException ex)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                    MessageBox.Show(this, $"Test connection failed: {ex.Message}", Globals.AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }
    }
}
