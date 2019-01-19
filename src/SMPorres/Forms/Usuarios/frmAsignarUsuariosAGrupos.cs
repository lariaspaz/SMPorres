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

namespace SMPorres.Forms.Usuarios
{
    public partial class frmAsignarUsuariosAGrupos : FormBase
    {
        public frmAsignarUsuariosAGrupos()
        {
            InitializeComponent();
            cbGrupos.DataSource = GruposRepository.ObtenerGrupos().OrderBy(g => g.Descripcion).ToList();
            cbGrupos.DisplayMember = "Descripcion";
            cbGrupos.ValueMember = "Id";
        }

        private int IdGrupo
        {
            get
            {
                return ((Models.Grupos)cbGrupos.SelectedItem).Id;
            }
        }

        private void cbGrupos_SelectedValueChanged(object sender, EventArgs e)
        {
            ConsultarUsuarios();
        }

        private void ConsultarUsuarios()
        {
            var asignados = GruposUsuariosRepository.ObtenerUsuariosPorGrupoId(IdGrupo).ToList();
            lbAsignados.DataSource = asignados;
            lbAsignados.ValueMember = "Id";
            lbAsignados.DisplayMember = "Nombre";
            var sinAsignar = UsuariosRepository.ObtenerUsuarios().Where(u => !asignados.Any(u2 => u2.Id == u.Id)).ToList();
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
            var idUsuario = (int)lbSinAsignar.SelectedValue;
            GruposUsuariosRepository.Insertar(IdGrupo, idUsuario);
            ConsultarUsuarios();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            var idUsuario = (int)lbAsignados.SelectedValue;
            GruposUsuariosRepository.Eliminar(IdGrupo, idUsuario);
            ConsultarUsuarios();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
