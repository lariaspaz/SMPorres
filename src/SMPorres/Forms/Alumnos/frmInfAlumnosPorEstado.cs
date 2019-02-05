using SMPorres.Lib.AppForms;
using SMPorres.Models;
using SMPorres.Prints.Reporte;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SMPorres.Repositories;

namespace SMPorres.Forms.Alumnos
{
    public partial class frmInfAlumnosPorEstado : FormBase
    {
        public frmInfAlumnosPorEstado(int idCarrera, int idCurso, EstadoAlumno estado)
        {
            InitializeComponent();

            var tabla = CrearDataTable();
            var alumnos = AlumnosRepository.ObtenerAlumnosPorEstado(idCarrera, idCurso, estado);

            foreach (var a in alumnos)
            {
                tabla.Rows.Add(a.Curso, a.Nombre, a.Apellido, a.EMail, a.LeyendaEstado());
            }

            AlumnosPorEstado cr = new AlumnosPorEstado();
            cr.Database.Tables["dtAlumnosPorEstado"].SetDataSource(tabla);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = cr;
        }

        private DataTable CrearDataTable()
        {
            DataTable dtAlumnosPorEstado = new DataTable();
            dtAlumnosPorEstado.Columns.Add("CarreraCurso", typeof(string));
            dtAlumnosPorEstado.Columns.Add("Nombre", typeof(string));
            dtAlumnosPorEstado.Columns.Add("Apellido", typeof(string));
            dtAlumnosPorEstado.Columns.Add("Email", typeof(string));
            dtAlumnosPorEstado.Columns.Add("Estado", typeof(string));

            return dtAlumnosPorEstado;
        }

        
    }
}
