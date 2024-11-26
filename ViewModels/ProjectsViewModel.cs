using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class ProjectsViewModel : AViewModel
    {
        public List<Project> Projects { get; set; }

        public ProjectsViewModel()
        {
            User user = Globals.User;

            try
            {
                Projects = Globals.DbContext.Set<Project>().Where(p => p.User == user).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
