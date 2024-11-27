using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ticksy.Dialogs;
using ticksy.Helpers;
using ticksy.ViewModels;

namespace ticksy.Views
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : UserControl
    {
        public Project Project { get; }
        public User User { get; }

        public TasksView(Project project, User user)
        {
            InitializeComponent();
            DataContext = new TasksViewModel(project);
            Project = project;
            User = user;
        }

        private void TbNewTask_TextChanged(object sender, TextChangedEventArgs e)
        {
            TbErrorNewTask.Text = "";
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {

            string errors = "";

            if (!isTaskDataValid(out errors))
            {
                Console.WriteLine("Validation failed: " + errors);
                // MessageBox.Show(this, $"Please enter valid data: {errors}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Console.WriteLine("Inputs validation successful");

            var newTask = new Task
            {
                ProjectId = Project.ProjectId,
                UserId = User.UserId,
                Name = TbNewTask.Text.Trim(),
                Status = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Console.WriteLine($"Name: {newTask.Name}, ProjectId: {newTask.ProjectId}, UserId: {newTask.UserId}, Status: {newTask.Status}, CreatedAt: {newTask.CreatedAt}, UpdatedAt: {newTask.UpdatedAt}");

            Globals.DbContext.Set<Task>().Add(newTask);
            Globals.DbContext.SaveChanges();

            // TODO: make a custom dialog
            // MessageBox.Show("Task saved successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            CustomMessageBox customMessageBox = new CustomMessageBox();
            customMessageBox.TbMessage.Text = "Task saved successfully!";
            var dashboard = Application.Current.Windows
        .OfType<Dashboard>() 
        .FirstOrDefault();

            if (dashboard != null)
            {
                customMessageBox.Owner = dashboard; // Set the Dashboard as the owner
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner; // Center the dialog
            }

            if (customMessageBox.ShowDialog() == true)
            {
                // Handle successful login
            }
            Console.WriteLine("Task saved successfully!");
            TbNewTask.Text = "";
        }

        public bool isTaskDataValid(out string errorsString) 
        {
            bool isValid = true;
            errorsString = "";

            if (!Validator.ValidateInput(TbNewTask.Text, "Task name", 5, 255, out string nameErrorMessage))
            {
                Validator.HandleValidationError(TbErrorNewTask, nameErrorMessage, out isValid);
                errorsString += " __ " + nameErrorMessage;
            }
            return isValid;
        }
    }
}
