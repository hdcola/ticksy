using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for LoginMainDlg.xaml
    /// </summary>
    public partial class LoginMainDlg : Window
    {
        public LoginMainDlg()
        {
            InitializeComponent();
        }

        private void BtnLoginWithEmail_Click(object sender, RoutedEventArgs e)
        {
            Close();
            LoginWithEmailDlg dialog = new LoginWithEmailDlg();
            dialog.Owner = Owner;
            dialog.ShowDialog();
        }

        private void BtnLoginWithGoogle_Click(object sender, RoutedEventArgs e)
        {
            // do Google Auth
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            Close();
            RegisterDlg dialog = new RegisterDlg();
            dialog.Owner = Owner;
            dialog.ShowDialog();
        }
    }
}
