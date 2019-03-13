using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Pagos
{
    public partial class frmLeerArchivoBSE : Form
    {
        string _path = null;

        public frmLeerArchivoBSE()
        {
            InitializeComponent();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "ISMP<ddmmyyy>"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 

           //Nullable<bool> result = 
                dlg.ShowDialog();
            _path = dlg.FileName.Trim();
            lPath.Text = _path;
            // Process open file dialog box results
            //if (result == true)
            //{
            //    // Open document
            //    string filename = dlg.FileName;
            //}
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            List<ArchivoBSE> archivo = new List<ArchivoBSE>();

            string[] lines = File.ReadAllLines(_path);

            foreach (string line in lines)
            {
                if (line.Contains("succod	sucursal	moneda	comprobante	tipo_mov	importe	fecha_proceso	cuil	usuario	hora	codbarra	grupoterminal	nrorendicion	fecha_cobro")) continue;
                MessageBox.Show(line);


            }
        }
    }
}
