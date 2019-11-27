using SMPorres.Models;
using System;
using System.Linq;

namespace SMPorres.Lib
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
                        _currentDate = dQuery.AsEnumerable().First();
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
    }
}
