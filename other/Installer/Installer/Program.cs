using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Installer.Lib.Security;
using System.Data.SqlClient;
using Installer.Controller;

namespace Installer
{
    public class Program
    {
        static void Main(string[] args)
        {
            InstallerController.EjecutarSMPorres();
        }
    }
}
