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

namespace SMPorres.Forms.MenuItems
{
    public partial class frmAsignarUsuariosyGruposAMenuItems : Form
    {
        public frmAsignarUsuariosyGruposAMenuItems()
        {
            InitializeComponent();
            //GenerarTreeView();
            cargarMenu();
        }

        private void cargarMenu()
        {
            var menu = ItemsMenuRepository.ObtenerItemsMenu().ToList();
            lbMenu.DataSource = menu;
            lbMenu.ValueMember = "Id";
            lbMenu.DisplayMember = "Descripcion";
        }

        private void GenerarTreeView()
        {
            //List<ItemsMenu> itemMenu = ItemsMenuRepository.ObtenerItemsMenu().ToList();
            //List<ItemsMenu> padres = ItemsMenuRepository.ObtenerPadresItemsMenu().ToList();

            //foreach (var it in padres)
            //{
            //    TreeNode node = new TreeNode(it.Descripcion);
            //    treeView1.Nodes.Add(node);
            //
            //    var hijos = ObtenerHijosPorId(it.Id);
            //    
            //    foreach (var h in hijos)
            //    {
            //        TreeNode nodoh = new TreeNode(h.Descripcion);
            //        treeView1.Nodes.Add(nodoh);
            //    }
            //}

            //foreach (var item in itemMenu)
            //{
            //    TreeNode nodoNuevo = new TreeNode(item.Descripcion);
            //    //Filtramos con LinQ para buscar si existe el nodo
            //    var nodo = (from TreeNode t in treeView1.Nodes
            //                where t.Text == nodoNuevo.Text
            //                select t).FirstOrDefault();
            //    //Si no existe, lo añadimos al treeView
            //    if (nodo == null)
            //    {
            //        treeView1.Nodes.Add(nodoNuevo);
            //    }
            //    else //si existe, lo añadimos al nodo que ya existe
            //    {
            //        nodo.Nodes.Add(nodoNuevo);
            //    }
            //}
        }

        //private void CrearNodoHijo(List<CategoriaJerarquica> categoriaList, TreeNode parentNode)
        //{
        //    categoriaList.ForEach(x =>
        //    {
        //    TreeNode node = new TreeNode(x.Descripcion);    //, Convert.ToString(x.IdCategoria));
        //
        //        if (x.CategoriaHija != null)
        //        {
        //            CrearNodoHijo(x.CategoriaHija, node);
        //        }
        //
        //        if (parentNode == null)
        //            treeView1.Nodes.Add(node);
        //        else
        //            parentNode.Nodes.Add(node);
        //    });
        //}

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
                if(rbUsuarios.Checked==true)
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
            }else
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

    }
}
