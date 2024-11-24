using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ticksy.Dialogs;

namespace ticksy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count = 0;

        public MainWindow()
        {
            InitializeComponent();

            txtCount.Text = count.ToString();
        }

        private void BtnTestDatabaseConn_OnClick(object sender, RoutedEventArgs e)
        {
            DbConfigDialog dialog = new DbConfigDialog();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            count++;
            txtCount.Text = count.ToString();
        }

        private void btnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectDlg dialog = new CreateProjectDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }

        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            CreateInvoiceDlg dialog = new CreateInvoiceDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginMainDlg dialog = new LoginMainDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginMainDlg dialog = new LoginMainDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }
    }
}
