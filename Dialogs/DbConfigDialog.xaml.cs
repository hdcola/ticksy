using System;
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
        public DbConfigDialog()
        {
            InitializeComponent();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            TbxPort.Text = DefaultPort;
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

                if (TestConnection())
                {
                    MessageBox.Show(this, "Test connection succeeded", "ticksy", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, $"Database connection couldn't be tested: {ex.Message}", "ticksy", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (!TestConnection()) return;

            // Save information to config file

            this.DialogResult = true;
        }

        private void TbxPort_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool TestConnection()
        {
            bool success = false;

            // Test connection here

            if (!success)
            {
                MessageBox.Show(this, "Error message, failed to connect", "ticksy", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return success;
        }
    }
}
