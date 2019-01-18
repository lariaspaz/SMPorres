using SMPorres.Lib.AppForms;
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
    public partial class frmAsignarAlumnosACursos : FormBase
    {
        public frmAsignarAlumnosACursos()
        {
            InitializeComponent();
            cbCarreras.DataSource = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
            lblCicloLectivo.Text = String.Format("Ciclo Lectivo {0:n0}", ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo);
        }

        private int IdCarrera
        {
            get
            {
                return ((Models.Carrera)cbCarreras.SelectedItem).Id;
            }
        }

        private int IdCurso
        {
            get
            {
                return ((Models.Curso)cbCursos.SelectedItem).Id;
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            var items = CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera).OrderBy(c => c.Nombre).ToList();
            cbCursos.DataSource = items;
            cbCursos.DisplayMember = "Nombre";
            cbCursos.ValueMember = "Id";
            cbCursos.SelectedItem = items.FirstOrDefault();
        }



        private void cbCursos_SelectedValueChanged(object sender, EventArgs e)
        {
            ConsultarAlumnos();
        }

        private void ConsultarAlumnos()
        {
            var asignados = CursosAlumnosRepository.ObtenerAlumnosPorCursoId(IdCurso).ToList();
            lbAsignados.DataSource = asignados;
            lbAsignados.ValueMember = "Id";
            lbAsignados.DisplayMember = "Nombre";
            var sinAsignar = AlumnosRepository.ObtenerAlumnos().Where(a => !asignados.Any(a2 => a2.Id == a.Id)).ToList();
            lbSinAsignar.DataSource = sinAsignar;
            lbSinAsignar.DisplayMember = "Nombre";
            lbSinAsignar.ValueMember = "Id";
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            var control = sender as SplitContainer;
            //paint the three dots'
            Point[] points = new Point[3];
            var w = control.Width;
            var h = control.Height;
            var d = control.SplitterDistance;
            var sW = control.SplitterWidth;

            //calculate the position of the points'
            if (control.Orientation == Orientation.Horizontal)
            {
                points[0] = new Point((w / 2), d + (sW / 2));
                points[1] = new Point(points[0].X - 10, points[0].Y);
                points[2] = new Point(points[0].X + 10, points[0].Y);
            }
            else
            {
                points[0] = new Point(d + (sW / 2), (h / 2));
                points[1] = new Point(points[0].X, points[0].Y - 10);
                points[2] = new Point(points[0].X, points[0].Y + 10);
            }

            foreach (Point p in points)
            {
                p.Offset(-2, -2);
                e.Graphics.FillEllipse(SystemBrushes.ControlDark,
                    new Rectangle(p, new Size(3, 3)));

                p.Offset(1, 1);
                e.Graphics.FillEllipse(SystemBrushes.ControlLight,
                    new Rectangle(p, new Size(3, 3)));
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            var idAlumno = (int) lbSinAsignar.SelectedValue;
            CursosAlumnosRepository.Insertar(IdCurso, idAlumno);
            ConsultarAlumnos();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var idAlumno = (int)lbAsignados.SelectedValue;
            CursosAlumnosRepository.Eliminar(IdCurso, idAlumno);
            ConsultarAlumnos();
        }
    }
}
