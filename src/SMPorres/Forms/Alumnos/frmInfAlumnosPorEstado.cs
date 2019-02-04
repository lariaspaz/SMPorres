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
        public frmInfAlumnosPorEstado()
        {
            InitializeComponent();
        }

        public frmInfAlumnosPorEstado(int idCarrera, int estado)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            var dataT = CrearDatatable();
            AlumnosRepository.cargarAlumnosPorCarreraEstado(dataT, idCarrera, estado);

            AlumnosPorEstado cr = new AlumnosPorEstado();
            cr.Database.Tables["dtAlumnosPorEstado"].SetDataSource(dataT);

            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = cr;
        }

        public frmInfAlumnosPorEstado(int idCarrera, int idCurso, int estado)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            var dataT = CrearDatatable();
            AlumnosRepository.cargarAlumnosPorCarreraCursoEstado(dataT, idCarrera, idCurso, estado);

            AlumnosPorEstado cr = new AlumnosPorEstado();
            cr.Database.Tables["dtAlumnosPorEstado"].SetDataSource(dataT);

            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = cr;
        }

        private DataTable CrearDatatable()
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
