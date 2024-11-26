using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ticksy.Helpers;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for LoginWithEmailDlg.xaml
    /// </summary>
    public partial class LoginWithEmailDlg : Window
    {
        // added to be able to access in unit tests
        public string EmailTest
        {
            get => TbEmail.Text;
            set => TbEmail.Text = value;
        }

        public string PasswordTest
        {
            get => TbPassword.Password;
            set => TbPassword.Password = value;
        }

        public LoginWithEmailDlg()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

            if (!isLoginDataValid(out errors))
            {
                Console.WriteLine("Validation failed: " + errors);
                // MessageBox.Show(this, $"Please enter valid data: {errors}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Console.WriteLine("Inputs validation successful");

            Dashboard dialog = new Dashboard();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }

        public bool isLoginDataValid(out string errorsString) 
        {
            bool isValid = true;
            errorsString = "";

            // validate Email
            string emailErrorMessage;
            if (!Validator.ValidateInput(TbEmail.Text, "Email", 1, 360, out emailErrorMessage) ||
                !Validator.ValidateEmail(TbEmail.Text, out emailErrorMessage))
            {
                Validator.HandleValidationError(TbErrorEmail, emailErrorMessage, out isValid);
                errorsString += " __ " + emailErrorMessage;
            }


            if (!Validator.ValidateInput(TbPassword.Password, "Password", out var passwordErrorMessage))
            {
                Validator.HandleValidationError(TbErrorPassword, passwordErrorMessage, out isValid);
                errorsString += " __ " + passwordErrorMessage;
            }

            return isValid;
        }

        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TbErrorPassword.Text = "";
        }

        private void TbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {            
            TbErrorEmail.Text = "";
        }
    }
}
