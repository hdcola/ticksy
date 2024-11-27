using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Navigation;
using System.Xml.Linq;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class TimeEntriesViewModel : AViewModel
    {
        private ObservableCollection<TimeEntrySummary> _timeEntries;
        public ObservableCollection<TimeEntrySummary> TimeEntries
        {
            get => _timeEntries;
            set
            {
                _timeEntries = value;
                OnPropertyChanged("TimeEntries");
            }
        }
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

                LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadData()
        {
            var entries = Globals.DbContext.Set<TimeEntry>()
                    .Where(t => t.UserId == User.UserId && t.StartTime > DateTime.Today)
                    .Select(t => new TimeEntrySummary
                    {
                        TimeEntryId = t.TimeEntryId,
                        Name = t.Name,
                        Task = t.Task,
                        StartTime = t.StartTime,
                        EndTime = t.EndTime
                    })
                    .ToList();

            TimeEntries = new ObservableCollection<TimeEntrySummary>(entries);
        }

        public void AddTimeEntry(TimeEntry newEntry)
        {
            try
            {
                Globals.DbContext.Set<TimeEntry>().Add(newEntry);
                Globals.DbContext.SaveChanges();

                LoadData();
                //TimeEntries.Add((TimeEntrySummary)created);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateTimeEntry()
        {
            try
            {
                Globals.DbContext.SaveChanges();

                LoadData();
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
