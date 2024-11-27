using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class TimeEntriesViewModel : AViewModel
    {
        public ObservableCollection<TimeEntrySummary> TimeEntries { get; set; }
        public List<Task> Tasks { get; set; }
        private User User { get; set; }

        public TimeEntriesViewModel(User user)
        {
            User = user;

            try
            {
                Tasks = Globals.DbContext.Set<Task>()
                    .Where(t => t.UserId == User.UserId)
                    .ToList();          

                var entries = Globals.DbContext.Set<TimeEntry>()
                    .Where(t => t.UserId == User.UserId && t.StartTime > DateTime.Today)
                    .Select(t => new TimeEntrySummary
                    {
                        Name = t.Name, Task = t.Task, StartTime = t.StartTime, EndTime = t.EndTime
                    })
                    .ToList();

                TimeEntries = new ObservableCollection<TimeEntrySummary>(entries);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    
    public class TimeEntrySummary : TimeEntry
    {
        public TimeSpan Elapsed { get => EndTime - StartTime; }
        public string TaskProject { get => $"{Task.Name}/{Task.Project.Name}"; }
    }
}
