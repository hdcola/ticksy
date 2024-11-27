   using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
using ticksy.Views;

namespace ticksy.Dialogs
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void TimeEntriesStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            User user = Globals.User;
            TabTimeEntries.Content = new TimeEntriesView(user, this);
        }

        private void ProjectsStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {   
            User user = Globals.User;
            TabProjects.Content = new ProjectsView(user);
        }

        private void BtnGenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            CreateInvoiceDlg dialog = new CreateInvoiceDlg();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                // Do something with the data
            }
        }
    }
}
