using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            TbErrorLogin.Text = "";
            string errors = "";

            if (!isLoginDataValid(out errors))
            {
                Console.WriteLine("Validation failed: " + errors);
                // MessageBox.Show(this, $"Please enter valid data: {errors}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Console.WriteLine("Inputs validation successful");

            try
            {
                // Get user matching email
                User user = Globals.DbContext.Set<User>().Where(u => u.Email == TbEmail.Text).FirstOrDefault();
                if (user == null)
                {
                    throw new ArgumentException("Invalid credentials");
                }

                var isValid = BCrypt.Net.BCrypt.Verify(TbPassword.Password, user.Password);
                if (isValid)
                {
                    Globals.User = user;
                    Console.WriteLine($"User {user.Username} is now logged in.");

                    Dashboard dialog = new Dashboard();
                    dialog.Show();
                    Close();
                }
                else
                {
                    throw new ArgumentException("Invalid credentials");
                }
            }
            catch (ArgumentException ex)
            {
                TbErrorLogin.Text = ex.Message;
            }
            catch (Exception ex)
            {
                // Failed to find user
                TbErrorLogin.Text = "Something went wrong. Try again later.";
                Console.WriteLine(ex.Message);
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

        private void BtnRegister_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            RegisterDlg dialog = new RegisterDlg();
            dialog.Owner = Owner;
            dialog.ShowDialog();
        }
    }
}
