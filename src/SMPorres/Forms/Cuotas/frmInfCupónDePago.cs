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

namespace SMPorres.Forms.Cuotas
{
    public partial class frmInfCupónDePago : FormBase
    {
        private int _idAlumno;

        private enum TipoImpresión
        {
            Matrícula,
            Cuota
        }

        public frmInfCupónDePago(int idAlumno)
        {
            InitializeComponent();

            _idAlumno = idAlumno;
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
            //var dt = CrearDataTable();
            //var alumnos = AlumnosRepository.ObtenerAlumnosPorEstado(IdCarrera, IdCurso, Estado);
            //foreach (var a in alumnos)
            //{
            //    dt.Rows.Add(String.Format("{0} de {1}", a.Curso, a.Carrera), a.Nombre, a.Apellido, a.EMail, a.LeyendaEstado());
            //}
            //return dt;

            var dt = new Reports.DataSet.dsImpresiones.CupónPagoDataTable();
            var row = (Reports.DataSet.dsImpresiones.CupónPagoRow)(dt.NewRow());
            row.IdPago = String.Format("{0:0000000}", 1);
            row.FechaEmisión = String.Format("0:dd/MM/yyyy", Lib.Configuration.CurrentDate);
            row.FechaVencimiento = dtFechaPago.Text;
            var alumno = AlumnosRepository.ObtenerAlumnoPorId(_idAlumno);
            row.Nombre = String.Format("{0} {1}", alumno.Nombre, alumno.Apellido);
            row.TipoDocumento = alumno.TipoDocumento.Descripcion;
            var carrera =
                (from ca in CursosAlumnosRepository.ObtenerCursosPorAlumno(_idAlumno)
                 join c in CarrerasRepository.ObtenerCarreras() on ca.IdCarrera equals c.Id
                 select c).FirstOrDefault();
            row.Carrera = carrera.Nombre;
            //row.Curso = 
            row.Concepto = "";
            row.Importe = String.Format("0:C2", 0);
            row.CódigoBarra = "";
            dt.Rows.Add(row);
            return dt;
        }

        private DataTable CrearDataTable()
        {
            var dt = new Reports.DataSet.dsImpresiones.CupónPagoDataTable();
            var rowCupónPagoRow = (Reports.DataSet.dsImpresiones.CupónPagoRow)(dt.NewRow());
            dt.Rows.Add(rowCupónPagoRow);
            return dt;
        }
    }
}
