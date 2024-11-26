using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ticksy.Helpers;

namespace ticksy.ViewModels
{
    public class TimeEntriesViewModel : AViewModel
    {
        public List<TimeEntry> TimeEntries { get; set; }
        private User User {  get; set; }

        public TimeEntriesViewModel(User user)
        {
            User = user;

            try
            {
                // We have to change database table structure and add UserId...
                TimeEntries = Globals.DbContext.Set<TimeEntry>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
