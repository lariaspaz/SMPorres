using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupDB
{
    class Program
    {
        static void Main(string[] args)
        {
            new Controllers.DriveController().SubirDB();
        }
    }
}
