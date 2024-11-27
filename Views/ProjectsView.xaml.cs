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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ticksy.Dialogs;
using ticksy.ViewModels;
using static ticksy.ViewModels.ProjectsViewModel;

namespace ticksy.Views
{
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : UserControl
    {
        private ProjectsViewModel viewModel { get; }
        public User User { get; }
        public ProjectsView(User user)
        {
            InitializeComponent();
            User = user;
            viewModel = new ProjectsViewModel(user);
            DataContext = viewModel;
        }


        private void BtnCreateProject_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectDlg dialog = new CreateProjectDlg(User);
            dialog.Owner = Window.GetWindow(this);

            if (dialog.ShowDialog() == true)
            {
                Console.WriteLine("Hello?");
                // Do something with the data
                Project project = dialog.Project;
                viewModel.AddProject(project);
            }
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            Project project = (Project)row.Item;
            this.Content = new TasksView(project, User);
        }
    }
}
