using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class TasksViewModel : AViewModel
    {
        public List<Task> Tasks { get; set; }

        private Project Project { get; set; }

        public TasksViewModel(Project project)
        {
            Project = project;

            try
            {
                Tasks = Globals.DbContext.Set<Task>().Where(p => p.ProjectId == Project.ProjectId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
