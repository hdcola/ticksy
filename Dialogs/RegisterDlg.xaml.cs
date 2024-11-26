using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for RegisterDlg.xaml
    /// </summary>
    public partial class RegisterDlg : Window
    {
        // added to be able to access in unit tests
        public string UsernameTest
        {
            get => TbUsername.Text;
            set => TbUsername.Text = value;
        }

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

        public string ConfirmPasswordTest
        {
            get => TbConfirmPassword.Password;
            set => TbConfirmPassword.Password = value;
        }

        public RegisterDlg()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string errors = "";

           
            if (!isRegisterDataValid(out errors))
            {
                Console.WriteLine("Validation failed: " + errors);
                // MessageBox.Show(this, $"Please enter valid data: {errors}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }
            Console.WriteLine("Validation successful");

            LoginWithEmailDlg dialog = new LoginWithEmailDlg();
            dialog.Show();
            this.Close();
        }
        

        public bool isRegisterDataValid(out string errorsString)
        {
            bool isValid = true;
            errorsString = "";

            // validate Username
            if (!Validator.ValidateInput(TbUsername.Text, "Username", 5, 50, out var usernameErrorMessage))
            {
                Validator.HandleValidationError(TbErrorUsername, usernameErrorMessage, out isValid);
                errorsString += " __ " + usernameErrorMessage;
            }


            // validate Email
            string emailErrorMessage;
            if (!Validator.ValidateInput(TbEmail.Text, "Email", 1, 360, out emailErrorMessage) ||
                !Validator.ValidateEmail(TbEmail.Text, out emailErrorMessage))
            {
                Validator.HandleValidationError(TbErrorEmail, emailErrorMessage, out isValid);
                errorsString += " __ " + emailErrorMessage;
            }

            // validate password
            if (!Validator.ValidateInput(TbPassword.Password, "Password", 6, 100, out var passwordErrorMessage))
            {
                Validator.HandleValidationError(TbErrorPassword, passwordErrorMessage, out isValid);
                errorsString += " __ " + passwordErrorMessage;
            }

            // validate confirm password
            if (TbPassword.Password != TbConfirmPassword.Password)
            {
                string confirmPasswordErrorMessage = "Passwords do not match.";
                Validator.HandleValidationError(TbErrorConfirmPassword, confirmPasswordErrorMessage, out isValid);
                errorsString += " __ " + confirmPasswordErrorMessage;
            }

            return isValid;
        }

        private async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetByteArrayAsync(imageUrl);
            }
        }

        private void TbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorUsername.Text = "";

        }

        private void TbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorEmail.Text = "";
        }


        private void TbConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorConfirmPassword.Text = "";
        }

        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TbErrorPassword.Text = "";
        }

        private void TbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TbErrorConfirmPassword.Text = "";
        }

        private void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            LoginMainDlg dialog = new LoginMainDlg();
            dialog.Show();
            this.Close();
        }
    }
}
