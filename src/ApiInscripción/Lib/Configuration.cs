using ApiInscripción.Models;
using System;
using System.Linq;

namespace ApiInscripción.Lib
{
    public static class Configuration
    {
        private static DateTime _currentDate;
        public static DateTime CurrentDate
        {
            get
            {
                using (var db = new SMPorresEntities())
                {
                    if (_currentDate == DateTime.MinValue)
                    {
                        var dQuery = db.Database.SqlQuery<DateTime>("SELECT GETDATE()");
                        _currentDate = dQuery.First();
                    }
                    return _currentDate;
                }
            }
        }

        public static short MaxCuotas
        {
            get
            {
                return 9;
            }
        }

        public static string Terminal
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public static string DBName
        {
            get
            {
                using (var db = new SMPorresEntities())
                {
                    return db.Database.Connection.Database;
                }
            }
        }

        public static string AppVersion
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }
    }
}
