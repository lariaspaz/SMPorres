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

        private void cargarDataTable(DataTable axcurso)
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
                                 Nombre = a.Nombre,
                                 Apellido = a.Apellido,
                                 NroDocumento = a.NroDocumento,
                                 FechaNacimiento = a.FechaNacimiento
                             }
                             ).ToList();

                foreach (var item in query)
                {
                    axcurso.Rows.Add(item.Curso, item.Nombre, item.NroDocumento, item.FechaNacimiento);
                }

            }
        }
       
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            DataTable axcurso = new DataTable();
            axcurso.Columns.Add("curso", typeof(string));
            axcurso.Columns.Add("nombre", typeof(string));
            axcurso.Columns.Add("apellido", typeof(string));
            axcurso.Columns.Add("fechanac", typeof(DateTime));

            cargarDataTable(axcurso);

            crAlumnos cr = new crAlumnos();
            cr.Database.Tables["axcurso"].SetDataSource(axcurso);

            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
