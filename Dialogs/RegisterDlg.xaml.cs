using PdfSharp.Pdf.Content.Objects;
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
        public RegisterDlg()
        {
            InitializeComponent();
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("BtnRegister_Click event");

            string errors = "";

            if (!isRegisterDataValid(out errors))
            {
                Console.WriteLine("Validation failed: " + errors);
                // MessageBox.Show(this, $"Please enter valid data: {errors}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(TbPassword.Text.Trim());

                string avatarUrl = "https://avatars.githubusercontent.com/u/161367934?v=4&size=40";
                byte[] avatarBytes = await DownloadImageAsync(avatarUrl);
                var newUser = new User
                {
                    Username = TbUsername.Text.Trim(),
                    Email = TbEmail.Text.Trim(),
                    Password = hashedPassword,
                    Avatar = avatarBytes,
                    CreatedAt = DateTime.Now
                };

                Globals.DbContext.Set<User>().Add(newUser); 
                Globals.DbContext.SaveChanges(); 

                MessageBox.Show(this, "Registration successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Console.WriteLine("Registration successful!");
                this.Close(); 
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Registration failed: {ex.Message}");

            }
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
            if (!Validator.ValidateInput(TbPassword.Text, "Email", 6, 100, out var passwordErrorMessage))
            {
                Validator.HandleValidationError(TbErrorPassword, passwordErrorMessage, out isValid);
                errorsString += " __ " + passwordErrorMessage;
            }

            // validate confirm password
            if (TbPassword.Text != TbConfirmPassword.Text)
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

        private void TbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorPassword.Text = "";
        }

        private void TbConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorConfirmPassword.Text = "";
        }
    }
}
