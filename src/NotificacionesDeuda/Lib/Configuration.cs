using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Lib
{
    public static class Configuration
    {
        public static string From
        {
            get
            {
                return ConfigurationManager.AppSettings["From"];
            }
        }

        public static string To
        {
            get
            {
                return ConfigurationManager.AppSettings["To"];
            }
        }

        public static string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["Host"];
            }
        }

        public static string User
        {
            get
            {
                return ConfigurationManager.AppSettings["User"];
            }
        }

        public static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Password"];
            }
        }

        public static int Port
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            }
        }

        public static bool EnableSsl
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            }
        }

        public static bool ValidateServerX509Certificate
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["ValidateServerX509Certificate"]);
            }
        }

        public static int MaxCount
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["MaxCount"]);
            }
        }

        public static string DisplayName
        {
            get
            {
                return ConfigurationManager.AppSettings["DisplayName"];
            }
        }
    }
}
