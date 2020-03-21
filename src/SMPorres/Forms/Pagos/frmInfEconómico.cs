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
    public partial class frmInfEconómico : FormBase
    {
        public frmInfEconómico()
        {
            InitializeComponent();
            cargarCicloLectivo();
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
            using (var reporte = new InformeEconómico())
            {
                string título = this.Text;
                var subTítulo = "Ciclo Lectivo " + cbCicloLectivo.Text;
                reporte.Database.Tables["InformeEconómico"].SetDataSource(dt);
                using (var f = new frmReporte(reporte, título, subTítulo)) f.ShowDialog();
            }
        }

        private void cargarCicloLectivo()
        {
            var qry = Repositories.CursosAlumnosRepository.ObtenerCiclosLectivos();
            cbCicloLectivo.DisplayMember = "CicloLectivo";
            //cbCicloLectivo.ValueMember = "Id";
            cbCicloLectivo.DataSource = qry;
        }

        private DataTable ObtenerDatos()
        {
            var tabla = new dsImpresiones.InformeEconómicoDataTable();
            var pagos = StoredProcs.ConsInformeEconómico(Convert.ToInt16(cbCicloLectivo.Text));
            foreach (var p in pagos)
            {
                tabla.AddInformeEconómicoRow(p.nroCuota, p.importeCuota, Convert.ToDecimal(p.impCuotas), Convert.ToDecimal(p.impPagoTer),
                    Convert.ToDecimal(p.impBeca), Convert.ToDecimal(p.impRec), Convert.ToDecimal(p.impPag), Convert.ToInt16(p.cantidadCuotas),
                    Convert.ToInt16(p.cantidadCuotasPagadas), Convert.ToInt16(p.cantidadCuotasAdeudadas));
            }
            return tabla;
        }
    }
}
