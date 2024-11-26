using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class TimeEntriesViewModel : AViewModel
    {
        public ObservableCollection<TimeEntrySummary> TimeEntries { get; set; }
        private User User { get; set; }

        public TimeEntriesViewModel(User user)
        {
            User = user;

            try
            {
                var entries = Globals.DbContext.Set<TimeEntry>()
                    .Where(t => t.UserId == User.UserId)
                    .Select(t => new TimeEntrySummary
                    {
                        Name = t.Name, Task = t.Task.Name, StartTime = t.StartTime, EndTime = t.EndTime
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
    
    public class TimeEntrySummary
    {
        public string Name { get; set; }
        public string Task { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Elapsed { get => EndTime - StartTime; }
    }
}
