using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPorres.Models;
using SMPorres.Repositories;
using static SMPorres.Repositories.ItemsMenuRepository;
using SMPorres.Lib.AppForms;

namespace SMPorres.Forms.MenuItems
{
    public partial class frmAsignarUsuariosyGruposAMenuItems : FormBase
    {
        public frmAsignarUsuariosyGruposAMenuItems()
        {
            InitializeComponent();
            //GenerarTreeView();
            CargarMenu();
        }

        private void CargarMenu()
        {
            var menu = ItemsMenuRepository.ObtenerItemsMenu().ToList();
            lbMenu.DataSource = menu;
            lbMenu.ValueMember = "Id";
            lbMenu.DisplayMember = "Descripcion";
        }

        private int IdGrupo
        {
            get
            {
                return (int)lbSinAsignar.SelectedValue;
            }
        }

        private int IdUsuario
        {
            get
            {
                return (int)lbSinAsignar.SelectedValue;
            }
        }

        private void rbGrupos_CheckedChanged(object sender, EventArgs e)
        {
            lbSinAsignar.DataSource = GruposRepository.ObtenerGrupos();
            lbSinAsignar.DisplayMember = "Descripcion";
            lbSinAsignar.ValueMember = "Id";
            ConsultarGrupos((int)lbSinAsignar.SelectedValue);
        }

        private void ConsultarGrupos(int idGrupo)
        {
            var sinAsignar = GruposRepository.ObtenerGrupos();
            lbSinAsignar.DataSource = sinAsignar;
            lbSinAsignar.DisplayMember = "Descripcion";
            lbSinAsignar.ValueMember = "Id";
            lbSinAsignar.SelectedIndex = idGrupo - 1;

            var asignados = GruposItemsMenuRepository.ObtenerItemsMenuPorGrupoId(idGrupo).ToList();
            lbAsignados.DataSource = asignados;
            lbAsignados.ValueMember = "Id";
            lbAsignados.DisplayMember = "Descripcion";
            labelAsignados.Text = "Asignados a " + lbSinAsignar.Text;

            var itemsMenu = ItemsMenuRepository.ObtenerItemsMenu().Where(u => !asignados.Any(u2 => u2.Id == u.Id)).ToList();
            lbMenu.DataSource = itemsMenu;
            lbMenu.DisplayMember = "Descripcion";
            lbMenu.ValueMember = "Id";
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            using (var db = new Models.SMPorresEntities())
            {
                if (rbGrupos.Checked == true)
                {
                    var idItemMenu = ItemsMenuRepository.ObtenerItemMenuPorDescripcion(lbMenu.Text);
                    if (idItemMenu == null || IdGrupo <= 0) return;
                    GruposItemsMenuRepository.Insertar(IdGrupo, idItemMenu.Id);
                    ConsultarGrupos(IdGrupo);
                }
                if (rbUsuarios.Checked == true)
                {
                    var idItemMenu = ItemsMenuRepository.ObtenerItemMenuPorDescripcion(lbMenu.Text);
                    if (idItemMenu == null || IdUsuario <= 0) return;
                    UsuariosItemsMenuRepository.Insertar(IdUsuario, idItemMenu.Id);
                    ConsultarUsuarios(IdUsuario);
                }
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (rbGrupos.Checked == true)
            {
                var idItemMenu = ItemsMenuRepository.ObtenerItemMenuPorDescripcion(lbAsignados.Text);
                if (idItemMenu == null || IdUsuario <= 0) return;
                GruposItemsMenuRepository.Eliminar(IdGrupo, idItemMenu.Id);
                ConsultarGrupos(IdGrupo);
            }
            if (rbUsuarios.Checked == true)
            {
                var idItemMenu = ItemsMenuRepository.ObtenerItemMenuPorDescripcion(lbAsignados.Text);
                if (idItemMenu == null || IdUsuario <= 0) return;
                UsuariosItemsMenuRepository.Eliminar(IdUsuario, idItemMenu.Id);
                ConsultarUsuarios(IdUsuario);
            }

        }

        private void lbSinAsignar_DoubleClick(object sender, EventArgs e)
        {
            if (rbGrupos.Checked == true)
            {
                labelAsignados.Text = "Asignados a " + lbSinAsignar.Text;
                ConsultarGrupos(IdGrupo);
            }
            else
            {
                labelAsignados.Text = "Asignados a " + lbSinAsignar.Text;
                ConsultarUsuarios(IdUsuario);
            }
        }

        private void rbUsuarios_CheckedChanged(object sender, EventArgs e)
        {
            lbSinAsignar.DataSource = UsuariosRepository.ObtenerUsuarios();
            lbSinAsignar.DisplayMember = "Nombre";
            lbSinAsignar.ValueMember = "Id";
            labelAsignados.Text = "Asignados a " + lbSinAsignar.Text;
            ConsultarUsuarios((int)lbSinAsignar.SelectedValue);
        }

        private void ConsultarUsuarios(int idUsuario)
        {
            var sinAsignar = UsuariosRepository.ObtenerUsuarios();
            lbSinAsignar.DataSource = sinAsignar;
            lbSinAsignar.DisplayMember = "Nombre";
            lbSinAsignar.ValueMember = "Id";
            lbSinAsignar.SelectedIndex = idUsuario - 1;

            var asignados = UsuariosItemsMenuRepository.ObtenerItemsMenuPorUsuarioId(idUsuario).ToList();
            lbAsignados.DataSource = asignados;
            lbAsignados.ValueMember = "Id";
            lbAsignados.DisplayMember = "Descripcion";
            labelAsignados.Text = "Asignados a " + lbSinAsignar.Text;

            var itemsMenu = ItemsMenuRepository.ObtenerItemsMenu().Where(u => !asignados.Any(u2 => u2.Id == u.Id)).ToList();
            lbMenu.DataSource = itemsMenu;
            lbMenu.DisplayMember = "Descripcion";
            lbMenu.ValueMember = "Id";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
