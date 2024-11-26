using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for AboutDlg.xaml
    /// </summary>
    public partial class AboutDlg : Window
    {
        private int count = 0;

        public AboutDlg()
        {
            InitializeComponent();
            SetVersionInfo();
        }

        private void SetVersionInfo()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionTextBlock.Text = $"Version {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /*
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            count++;
            txtCount.Text = count.ToString();
        }*/
    }
}
