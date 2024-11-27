using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class ProjectsViewModel : AViewModel
    {
        public ObservableCollection<ProjectSummary> ProjectSummaries { get; set; }

        private User User { get; set; }

        public ProjectsViewModel(User user)
        {
            User = user;

            try
            {
                var projects = Globals.DbContext.Set<Project>()
                    .Where(p => p.UserId == User.UserId)
                    .Include(p => p.Tasks)
                    .Include("Tasks.TimeEntries")
                    .Select(p => new ProjectSummary
                    {
                        ProjectId = p.ProjectId,
                        Name = p.Name,
                        Description = p.Description,
                        HourlyRate = p.HourlyRate,
                        TotalTrackedHours = p.Tasks
                            .SelectMany(t => t.TimeEntries)
                            .Where(te => te.StartTime != null && te.EndTime != null)
                            .Sum(te => (int?)DbFunctions.DiffHours(te.StartTime, te.EndTime) ?? 0),
                        Amount = p.Tasks
                            .SelectMany(t => t.TimeEntries)
                            .Where(te => te.StartTime != null && te.EndTime != null)
                            .Sum(te => ((int?)DbFunctions.DiffHours(te.StartTime, te.EndTime) ?? 0) * (double)p.HourlyRate)
                    })
                    .ToList();

                ProjectSummaries = new ObservableCollection<ProjectSummary>(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while fetching projects: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }


        }
        public class ProjectSummary : Project
        {
            public int? TotalTrackedHours { get; set; }
            public double? Amount { get; set; }
        }
    }
}
