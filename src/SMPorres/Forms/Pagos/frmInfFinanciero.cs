using SMPorres.Lib.AppForms;
using SMPorres.Reports.DataSet;
using SMPorres.Reports.Designs;
using SMPorres.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Pagos
{
    public partial class frmInfFinanciero : FormBase
    {
        public frmInfFinanciero()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            using (var dt = ObtenerDatos())
            {
                if (dt.Rows.Count > 0)
                {
                    MostrarReporte(dt);
                }
                else
                {
                    ShowError("No hay ningún registro que coincida con su consulta.");
                }
            }
        }

        private void MostrarReporte(DataTable dt)
        {
            using (var reporte = new InformeFinanciero())
            {
                string título = this.Text;
                string período = String.Format("Desde {0:dd/MM/yy} al {1:dd/MM/yy} ", dtDesde.Value.Date, dtHasta.Value.Date);
                var subTítulo = período;

                reporte.Database.Tables["InformeFinanciero"].SetDataSource(dt);
                using (var f = new frmReporte(reporte, título, subTítulo)) f.ShowDialog();
            }
        }

        private DataTable ObtenerDatos()
        {
            var tabla = new dsImpresiones.InformeFinancieroDataTable();
            var pagos = StoredProcs.ConsInformeFinanciero(dtDesde.Value, dtHasta.Value);
            foreach (var p in pagos)
            {
                tabla.AddInformeFinancieroRow(p.carrera, p.curso, Convert.ToInt16( p.Cuotas ), 
                    Convert.ToDecimal( p.Importe ), Convert.ToDecimal ( p.pagoTermino ), 
                    Convert.ToDecimal( p.Beca ), Convert.ToDecimal( p.Recargo ), 
                    Convert.ToDecimal( p.Pagado) );
            }
            return tabla;
        }
    }
}
