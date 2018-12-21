using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Lib.Components
{
    public static class Forms
    {
        public static void InitForm(System.Windows.Forms.Form f)
        {
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.ShowInTaskbar = false;
            f.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
