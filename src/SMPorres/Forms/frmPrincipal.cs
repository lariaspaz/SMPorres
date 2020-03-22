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
using SMPorres.Repositories;
using SMPorres.Models;
using SMPorres.Forms.Pagos;

namespace SMPorres.Forms
{
    public partial class frmPrincipal : Form
    {
        IList<string> _menuItems;
        IList<ItemsMenu> _permisos;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void CargarPermisosGruposDeUsuarioActual(int idusuario)
        {
            List<Grupos> grupos = new List<Grupos>();
            grupos = GruposUsuariosRepository.ObtenerGruposPorIdUsuario(idusuario);

            if (grupos == null) return;

            foreach (var item in grupos)
            {
                List<ItemsMenu> itemsMenu = new List<ItemsMenu>();
                itemsMenu = (List<ItemsMenu>)GruposItemsMenuRepository.ObtenerItemsMenuPorIdGrupo(item.Id);

                foreach (var i in itemsMenu)
                {
                    if (!_permisos.Contains(i))
                    {
                        _permisos.Add(i);
                    }
                }

            }
        }

        private void CargarPermisosUsuarioActual(int idusuario)
        {
            _permisos = ItemsMenuRepository.ObtenerItemsMenu(idusuario);
        }

        private void ArmarMenu(ToolStripItemCollection items)
        {
            foreach (var i in items)
            {
                if (i is ToolStripMenuItem)
                {
                    var m = (ToolStripMenuItem)i;
                    m.Enabled = _permisos.Any(p => p.Nombre == m.Name);
                    m.Visible = m.Enabled;
                    ArmarMenu(m.DropDownItems);
                }
            }
            archivoToolStripMenuItem.Enabled = true;
            archivoToolStripMenuItem.Visible = true;
            salirToolStripMenuItem.Enabled = true;
            salirToolStripMenuItem.Visible = true;
        }

        private void RecorrerMenu(ToolStripItemCollection items, string nombrePadre)
        {
            foreach (var m in items)
            {
                if (m is ToolStripMenuItem)
                {
                    var m1 = (ToolStripMenuItem)m;
                    _menuItems.Add(m1.Name);
                    ItemsMenuRepository.Actualizar(m1.Name, m1.Text, nombrePadre);
                    this.RecorrerMenu(m1.DropDownItems, m1.Name);
                }
            }
        }

        private void carrerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Carreras.frmListado()) f.ShowDialog();
        }

        internal bool Inicializar()
        {
            if (new frmLogin().ShowDialog() == DialogResult.OK)
            {
                _menuItems = new List<string>();
                RecorrerMenu(this.menuStrip1.Items, null);
                ItemsMenuRepository.EliminarItemsInexistentes(_menuItems);
                Icon = Properties.Resources.app2;
                lblTerminal.Text = Lib.Configuration.Terminal;
                lblUsuario.Text = Lib.Session.CurrentUser.NombreCompleto;
                lblBaseDatos.Text = Lib.Configuration.DBName;
                lblExeVersion.Text = "v. " + Lib.Configuration.AppVersion;

                int idUsuario = Lib.Session.CurrentUser.Id;
                CargarPermisosUsuarioActual(idUsuario);
                CargarPermisosGruposDeUsuarioActual(idUsuario);
                ArmarMenu(menuStrip1.Items);
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

        private void configuraciónGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmConfiguración()) f.ShowDialog();
        }

        private void asignarPermisosAGruposYUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new MenuItems.frmAsignarUsuariosyGruposAMenuItems()) f.ShowDialog();
        }

        private void departamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Departamentos.frmListado()) f.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            using (var f = new Localidades.frmListado()) f.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            using (var f = new Barrios.frmListado()) f.ShowDialog();
        }

        private void alumnosPorEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Alumnos.frmInfAlumnosPorEstado()) f.ShowDialog();
        }

        private void alumnosMorososToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Alumnos.frmInfAlumnosMorosos()) f.ShowDialog();
        }

        private void PagosTotalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Pagos.frmInfPagosTotales()) f.ShowDialog();
        }

        private void rendiciónBSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Pagos.frmImportarPagosBSE()) f.ShowDialog();
        }

        private void actualizarDatosEnLaWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new Web.frmActualizarDatos()) f.ShowDialog();
        }

        private void enviarEmailAAlumnosMorososToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (var f = new Alumnos.frmEmailAlumnosMorosos()) f.ShowDialog();
        }

        private void económicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmInfEconómico()) f.ShowDialog();
        }

        private void financieroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frmInfFinanciero()) f.ShowDialog();
        }
    }
}
