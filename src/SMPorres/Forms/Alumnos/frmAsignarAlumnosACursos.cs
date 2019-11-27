using SMPorres.Lib.AppForms;
using SMPorres.Models;
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
        private string _leyenda = "Buscar por nombre";
        private IEnumerable<Alumno> _sinAsignar;

        public frmAsignarAlumnosACursos()
        {
            InitializeComponent();
            cbCarreras.DataSource = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";

            lblCicloLectivo.Text = String.Format("Ciclo Lectivo {0:n0}", ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo);

            txtBuscar.ForeColor = SystemColors.GrayText;
            txtBuscar.Text = _leyenda;
            this.txtBuscar.Leave += new System.EventHandler(this.textBox1_Leave);
            this.txtBuscar.Enter += new System.EventHandler(this.textBox1_Enter);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Length == 0)
            {
                txtBuscar.Text = _leyenda;
                txtBuscar.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == _leyenda)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = SystemColors.WindowText;
            }
        }

        private int IdCarrera
        {
            get
            {
                return ((Carrera)cbCarreras.SelectedItem).Id;
            }
        }

        private int IdCurso
        {
            get
            {
                if (cbCursos.SelectedItem == null) return 0;
                return ((Curso)cbCursos.SelectedItem).Id;
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            var items = CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera).OrderBy(c => c.Nombre).ToList();
            cbCursos.DisplayMember = "Nombre";
            cbCursos.ValueMember = "Id";
            cbCursos.DataSource = items;
            //cbCursos.SelectedIndex = 1;
            ConsultarAlumnos();
        }



        private void cbCursos_SelectedValueChanged(object sender, EventArgs e)
        {
            ConsultarAlumnos();
        }

        private void ConsultarAlumnos()
        {
            var asignados = from a in CursosAlumnosRepository.ObtenerAlumnosPorCursoId(IdCurso)
                            select new Alumno {
                                Id = a.Id,
                                Nombre = String.Format("{0} {1} - {2} {3}", a.TipoDocumento.Descripcion, 
                                            a.NroDocumento, a.Nombre, a.Apellido)
                            };
            lbAsignados.DataSource = asignados.ToList();
            lbAsignados.ValueMember = "Id";
            lbAsignados.DisplayMember = "Nombre";
            btnQuitar.Enabled = asignados.Any();

            _sinAsignar = from a in AlumnosRepository.ObtenerAlumnos()
                              where a.Estado == (byte)EstadoAlumno.Activo &&
                                    !asignados.Any(a2 => a2.Id == a.Id)
                              select new Alumno
                              {
                                  Id = a.Id,
                                  Nombre = String.Format("{0} {1} - {2} {3}", a.TipoDocumento.Descripcion,
                                            a.NroDocumento, a.Nombre, a.Apellido)
                              };

            //lbSinAsignar.DataSource = _sinAsignar.ToList();
            //lbSinAsignar.DisplayMember = "Nombre";
            //lbSinAsignar.ValueMember = "Id";
            //btnAsignar.Enabled = _sinAsignar.Any();

            Filtrar();

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
            var idAlumno = (int)lbSinAsignar.SelectedValue;
            CursosAlumnosRepository.Insertar(IdCurso, idAlumno);
            ConsultarAlumnos();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var idAlumno = (int)lbAsignados.SelectedValue;
            CursosAlumnosRepository.Eliminar(IdCurso, idAlumno);
            ConsultarAlumnos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Filtrar()
        {
            
            var data = _sinAsignar;
            if (txtBuscar.Text != _leyenda)
            {
                data = _sinAsignar.Where(t => t.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper()));
            }
            lbSinAsignar.DataSource = data.ToList();
            lbSinAsignar.DisplayMember = "Nombre";
            lbSinAsignar.ValueMember = "Id";
            btnAsignar.Enabled = data.Any();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Filtrar();
        }
    }
}
