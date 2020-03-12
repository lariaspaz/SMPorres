using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilidades
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void configurarCadenaDeConexiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmConfigurarCadenaConexión())
            {
                f.ShowDialog();
            }
        }

        private void testerDelWebServiceDeConsultasWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmConsultasWeb())
            {
                f.ShowDialog();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var f = new frmTestApiInscripción())
            {
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
            }
        }
    }
}
