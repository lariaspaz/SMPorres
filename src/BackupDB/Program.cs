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
            //var c = new Controllers.DriveController();
            //int i = 0;
            //while (i < 3)
            //{
            //    if (i > 0) System.Threading.Thread.Sleep(TimeSpan.FromMinutes(1));
            //    c.SubirDB();
            //    i++;
            //}

            new Controllers.DriveController().SubirDB();
        }
    }
}
