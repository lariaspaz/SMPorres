using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Installer.Lib
{
    public static class Configuration
    {
        public static string Environment
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"];
            }
        }

    }
}

   