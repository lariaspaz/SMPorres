using SMPorres.Lib.AppForms;
using SMPorres.Models;
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

namespace SMPorres.Forms.Alumnos
{
    public partial class frmInfCupónDePago : FormBase
    {
        private enum TipoImpresión
        {
            Matrícula,
            Cuota
        }

        public frmInfCupónDePago()
        {
            InitializeComponent();

            var tipos = new Dictionary<TipoImpresión, string>();
            tipos.Add(TipoImpresión.Cuota, "Cuota");
            tipos.Add(TipoImpresión.Matrícula, "Matrícula");
            cbTipo.DataSource = new BindingSource(tipos, null);
            cbTipo.ValueMember = "Key";
            cbTipo.DisplayMember = "Value";
            cbTipo.SelectedIndex = 0;
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
                    ShowError("No hay datos para mostrar.");
                }
            }
        }

        private static void MostrarReporte(DataTable dt)
        {
            using (var reporte = new CupónDePago())
            {
                reporte.Database.Tables["dtAlumno"].SetDataSource(dt);
                using (var f = new frmReporte("Informe de Alumnos por Estado", reporte)) f.ShowDialog();
            }
        }

        private TipoImpresión Tipo
        {
            get
            {
                return (TipoImpresión)cbTipo.SelectedValue;
            }
        }

        private DataTable ObtenerDatos()
        {
            var dt = CrearDataTable();
            //var alumnos = AlumnosRepository.ObtenerAlumnosPorEstado(IdCarrera, IdCurso, Estado);
            //foreach (var a in alumnos)
            //{
            //    dt.Rows.Add(String.Format("{0} de {1}", a.Curso, a.Carrera), a.Nombre, a.Apellido, a.EMail, a.LeyendaEstado());
            //}
            return dt;
        }

        private DataTable CrearDataTable()
        {
            var alumno = new DataTable();
            alumno.Columns.Add("Nombre", typeof(string));
            alumno.Columns.Add("Domicilio", typeof(string));
            alumno.Columns.Add("Localidad", typeof(string));
            alumno.Columns.Add("Documento", typeof(string));

            var detalle = new DataTable();
            detalle.Columns.Add("Cantidad", typeof(string));
            detalle.Columns.Add("Descripción", typeof(string));
            detalle.Columns.Add("Precio", typeof(string));
            detalle.Columns.Add("Importe", typeof(string));

            var impresion = new DataTable();
            impresion.Columns.Add("comprobantePago", typeof(string));
            impresion.Columns.Add("fechaVencimiento", typeof(string));
            impresion.Columns.Add("codigoBarra", typeof(string));
            impresion.Columns.Add("Total", typeof(string));

            return alumno;
        }
    }
}
