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
        public LoginWithEmailDlg()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            // bool isValidPassword = BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
            /*
            if (!isLoginDataValid())
            {
                Console.WriteLine("Invalid ");
            }
            */

        }

        /*
        public bool isLoginDataValid() 
        {
            bool isValid = true;

            // validate Email
            string emailErrorMessage;
            if (!Validator.ValidateInput(TbEmail.Text, "Email", 1, 360, out emailErrorMessage) ||
                !Validator.ValidateEmail(TbEmail.Text, out emailErrorMessage))
            {
                Validator.HandleValidationError(TbErrorEmail, emailErrorMessage, out isValid);
            }


            return isValid;
        }
        */
    }
}
