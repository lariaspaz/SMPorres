using SMPorres.Prints.Reporte;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SMPorres.Prints
{
    public partial class ListadoAlumnosXCurso : Lib.AppForms.FormBase
    {
        public ListadoAlumnosXCurso()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            var dataT = CrearDatatable();
            cargarDatos(dataT);

            AlumnosPorCurso cr = new AlumnosPorCurso();
            cr.Database.Tables["dtAlumnosXCurso"].SetDataSource(dataT);

            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = cr;
        }

        private DataTable CrearDatatable()
        {
            DataTable dtAlumnosXCurso = new DataTable();
            dtAlumnosXCurso.Columns.Add("curso", typeof(string));
            dtAlumnosXCurso.Columns.Add("apellido", typeof(string));
            dtAlumnosXCurso.Columns.Add("nombre", typeof(string));
            dtAlumnosXCurso.Columns.Add("documento", typeof(string));
            dtAlumnosXCurso.Columns.Add("telefono", typeof(string));

            return dtAlumnosXCurso;
        }

        private void cargarDatos(DataTable x)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var query = (from a in db.Alumnos
                             join ac in db.CursosAlumnos
                             on a.Id equals ac.IdAlumno
                             join c in db.Cursos
                             on ac.IdCurso equals c.Id
                             select new
                             {
                                 Curso = c.Nombre,
                                 Apellido = a.Apellido,
                                 Nombre = a.Nombre,
                                 Documento = a.NroDocumento,
                                 Telefono = "0385-0303456"
                             }
                             ).ToList();

                foreach (var item in query)
                {
                    x.Rows.Add(item.Curso, item.Apellido, item.Nombre, item.Documento, item.Telefono);
                }

            }
        }
    }
}
