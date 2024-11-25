using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (Globals.DbContext == null)
            {
                Console.WriteLine("DbContext is not initialized.");
                MessageBox.Show("Database connection is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LoadProjects();
            */
        }

        /*
        private void LoadProjects()
        {
            try
            {
                var projects = Globals.DbContext.Set<Project>()
                    .Include(p => p.Tasks)
                    .Include("Tasks.TimeEntries")
                    .Select(p => new
                    {
                        p.Name,
                        p.Description,
                        TotalTrackedHours = p.Tasks != null
                            ? p.Tasks
                                .SelectMany(t => t.TimeEntries ?? new List<TimeEntry>())
                                .Where(te => te.StartTime != null && te.EndTime != null)
                                .Sum(te => (double?)DbFunctions.DiffHours(te.StartTime, te.EndTime) ?? 0)
                            : 0,
                        p.HourlyRate
                    })
                    .ToList();


                LvProjects.ItemsSource = projects;




                Console.WriteLine("Projects loaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to load projects: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Failed to load projects: {ex.Message}");
            }
        }
        */

    }
}
