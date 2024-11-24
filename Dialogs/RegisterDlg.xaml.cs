﻿using System;
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

            // validate First Name
            if (!Validator.ValidateInput(TbFirstName.Text, "First name", 5, 50, out var firstNameErrorMessage))
            {
                Validator.HandleValidationError(TbErrorFirstName, firstNameErrorMessage, ref isValid);
            }

            // validate Last Name
            if (!Validator.ValidateInput(TbLastName.Text, "Last name", 5, 50, out var lastNameErrorMessage))
            {
                Validator.HandleValidationError(TbErrorLastName, lastNameErrorMessage, ref isValid);
            }

            // validate Email
            if (!Validator.ValidateEmail(TbEmail.Text, out var emailErrorMessage))
            {
                Validator.HandleValidationError(TbErrorEmail, emailErrorMessage, ref isValid);
            }

            return isValid;
        }

       

    }
}
