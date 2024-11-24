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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        public bool isLoginDataValid() 
        {
            bool isValid = true;
            string email = textBoxEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                // error text = "Please enter email"
                Console.WriteLine("Please enter email");
                isValid = false;
            }

            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch
            {
                // error text = "Please enter a valid email"
                Console.WriteLine("Please enter a valid email");
                isValid = false;
            }

            return isValid;
        }
    }
}
