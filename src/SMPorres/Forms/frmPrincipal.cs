using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPorres.Forms;

namespace SMPorres.Forms
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void carrerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Carreras.frmListado()) f.ShowDialog();
        }

        internal bool Inicializar()
        {
            if (new frmLogin().ShowDialog() == DialogResult.OK)
            {
                return true;
            }

            return false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Usuarios.frmListado()) f.ShowDialog();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmCambiarContraseña()) f.ShowDialog();
        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Alumnos.frmListado()) f.ShowDialog();
        }

        private void gruposDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new UsuariosGrupos.frmListado()) f.ShowDialog();
		}
		
        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Cursos.frmListado()) f.ShowDialog();
        }

        private void asignarAlumnosACursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Alumnos.frmAsignarAlumnosACursos()) f.ShowDialog();
        }

        private void asignarUsuariosAGruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Usuarios.frmAsignarUsuariosAGrupos()) f.ShowDialog();
        }

        private void panelDeAlumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Alumnos.frmPanelAlumno()) f.ShowDialog();
        }

        private void cuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Cuotas.frmListado()) f.ShowDialog();
        }
    }
}
