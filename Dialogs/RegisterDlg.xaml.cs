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
    /// Interaction logic for RegisterDlg.xaml
    /// </summary>
    public partial class RegisterDlg : Window
    {
        public RegisterDlg()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isRegisterDataValid()) { 
                Console.WriteLine("Please enter valid data");
                return;
            }
        }


        public bool isRegisterDataValid()
        {
            bool isValid = true;

            if (!Validator.ValidateString(TbFirstName.Text, "First name", 5, 50, out var errorMessage))
            {
                TbErrorFirstName.Text = errorMessage;
                Console.WriteLine(errorMessage);
                isValid = false;
            }

            return isValid;
        }
    }
}
