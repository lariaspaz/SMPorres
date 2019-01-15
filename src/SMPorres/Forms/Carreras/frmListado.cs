using CustomLibrary.Extensions.Controls;
using SMPorres.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SMPorres.Forms.Carreras
{
    public partial class frmListado : Lib.AppForms.FormBase
    {
        public frmListado()
        {
            InitializeComponent();
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            dgvDatos.SetDataSource(from c in CarrerasRepository.ObtenerCarreras()
                                   orderby c.Id
                                   select new
                                   {
                                       c.Id,
                                       c.Nombre,
                                       Duracion = String.Format("{0} años", c.Duracion),
                                       DescripciónEstado = (c.Estado == 1 ? "Habilitada" : "Baja"),
                                       c.Estado
                                   });
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Nombre";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[2].HeaderText = "Duración";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[2].DefaultCellStyle.Format = "d";

            dgvDatos.Columns[3].HeaderText = "Estado";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[4].Visible = false;
        }

        private void frmListado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var f = new frmEdición())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var c = CarrerasRepository.Insertar(f.Nombre, f.Duración, f.Importe, f.Estado);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == c.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Models.Carrera m = ObtenerCarreraSeleccionada();
            using (var f = new frmEdición(m))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        CarrerasRepository.Actualizar(m.Id, f.Nombre, f.Duración, f.Importe, f.Estado);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private Models.Carrera ObtenerCarreraSeleccionada()
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
            var m = CarrerasRepository.ObtenerCarreraPorId(id);
            return m;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Models.Carrera m = ObtenerCarreraSeleccionada();
            if (MessageBox.Show("¿Está seguro de que desea eliminar la carrera seleccionada?",
                "Eliminar carrera", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    CarrerasRepository.Eliminar(m.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }
    }
}
