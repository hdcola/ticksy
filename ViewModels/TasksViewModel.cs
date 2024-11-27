using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class TasksViewModel : AViewModel
    {
        private ObservableCollection<Task> _tasks;
        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        private Project Project { get; set; }

        public TasksViewModel(Project project)
        {
            Project = project;

            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadData()
        {
            var tasks = Globals.DbContext.Set<Task>().Where(p => p.ProjectId == Project.ProjectId).ToList();
            Tasks = new ObservableCollection<Task>(tasks);
        }

        public void AddTask(Task task)
        {
            try
            {
                Globals.DbContext.Set<Task>().Add(task);
                Globals.DbContext.SaveChanges();

                LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
