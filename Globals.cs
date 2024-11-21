using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticksy
{
    public class Globals
    {
        public const string AppName = "ticksy";

        // Returns the AppDataPath + AppName dir
        public static string GetAppDataPath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, AppName);
        }

        internal static DbContext DbContext;
    }
}
