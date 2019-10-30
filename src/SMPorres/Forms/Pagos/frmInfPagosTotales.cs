using SMPorres.Lib.AppForms;
using SMPorres.Models;
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
    public partial class frmInfPagosTotales : FormBase
    {
        private enum TiposInforme
        {
            AlumnosAlDía = 1,
            AlumnosMorosos = 2
        }

        public frmInfPagosTotales()
        {
            InitializeComponent();

            var qry = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            qry.Insert(0, new Carrera { Id = 0, Nombre = "(Todas las carreras)" });
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
            cbCarreras.DataSource = qry;

            cargarMediosPago();
        }

        private void cargarMediosPago()
        {
            var qry = MediosPagoRepository.ObtenerMediosPago().OrderBy(mp => mp.Descripcion).ToList();
            qry.Insert(0, new MedioPago { Id = 0, Descripcion = "(Todos los medios de pago)" });
            cbMedioPago.DisplayMember = "Descripcion";
            cbMedioPago.ValueMember = "Id";
            cbMedioPago.DataSource = qry;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
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
            //using (var reporte = new TotalPagos())
            using (var reporte = new TotalesPago())
            {
                string título = this.Text;
                string período = String.Format("Desde {0:dd/MM/yy} al {1:dd/MM/yy} ", dtDesde.Value.Date, dtHasta.Value.Date);
                string curso = cbCursos.Text;
                if (IdCurso == 0) curso = curso.Replace("(", "").Replace(")", "");
                string carrera = cbCarreras.Text;
                if (IdCarrera == 0) carrera = carrera.Replace("(", "").Replace(")", "");
                var subTítulo = período + " - " + curso + " - " + carrera;
                reporte.Database.Tables["TotalPago"].SetDataSource(dt);
                using (var f = new frmReporte(reporte, título, subTítulo)) f.ShowDialog();
            }
        }

        private int IdCarrera
        {
            get
            {
                return Convert.ToInt32(cbCarreras.SelectedValue);
            }
        }

        private int IdCurso
        {
            get
            {
                return Convert.ToInt32(cbCursos.SelectedValue);
            }
        }

        private int IdMedioPago
        {
            get
            {
                return Convert.ToInt32(cbMedioPago.SelectedValue);
            }
        }

        private DateTime Desde
        {
            get
            {
                return dtDesde.Value.Date;
            }
        }

        private DateTime Hasta
        {
            get
            {
                return dtHasta.Value.Date;
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            var items = CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera).OrderBy(c => c.Nombre).ToList();
            items.Insert(0, new Curso { Id = 0, Nombre = "(Todos los cursos)" });
            cbCursos.DataSource = items;
            cbCursos.DisplayMember = "Nombre";
            cbCursos.ValueMember = "Id";
            cbCursos.SelectedIndex = 0;
        }

        private DataTable ObtenerDatos()
        {
            var tabla = new dsImpresiones.TotalPagoDataTable();
            var pagos = StoredProcs.ConsTotalPagos(Desde, Hasta, IdCarrera, IdCurso, IdMedioPago);
            foreach (var p in pagos)
            {
                tabla.AddTotalPagoRow(p.Carrera, p.Curso, p.IdCarrera, p.IdCurso, p.MedioPago, p.Cantidad, p.Total );
            }
            return tabla;
        }
    }
}
