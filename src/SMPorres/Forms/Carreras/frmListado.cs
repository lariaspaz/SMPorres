using CustomLibrary.Extensions.Controls;
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
using SMPorres.Lib.Components;

namespace SMPorres.Forms.Carreras
{
    public partial class frmListado : Form
    {
        public frmListado()
        {
            InitializeComponent();
            Lib.Components.Forms.InitForm(this);
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            dgvDatos.SetDataSource(from c in CarrerasRepository.ObtenerCarreras()
                                   select new
                                   {
                                       c.Id,
                                       c.Nombre,
                                       c.Duracion,
                                       DescripciónEstado = (c.Estado == 1 ? "Habilitada" : "Baja"),
                                       c.Estado
                                   });
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                //dgvDatos.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.AliceBlue;
            }
            dgvDatos.Rows[e.RowIndex].Cells[0].Style.BackColor = SystemColors.ButtonFace;
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
                        var c = CarrerasRepository.Insertar(f.Nombre, f.Duración, f.Importe1Vto, 
                            f.Importe2Vto, f.Importe3Vto, f.Estado);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == c.Id);
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (decimal)dgvDatos.Rows[rowindex].Cells[0].Value;
            var m = CarrerasRepository.ObtenerCarreraPorId(id);
            using (var f = new frmEdición(m))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        CarrerasRepository.Actualizar(m.Id, f.Nombre, f.Duración, f.Importe1Vto, f.Importe2Vto,
                            f.Importe3Vto, f.Estado);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.ShowError(ex.Message);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (decimal)dgvDatos.Rows[rowindex].Cells[0].Value;
            var m = CarrerasRepository.ObtenerCarreraPorId(id);
            if (MessageBox.Show("¿Está seguro de que desea contrasentar el movimiento seleccionado?",
                "Contrasentar Movimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    CarrerasRepository.Eliminar(m.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowError(ex.Message);
                }
            }
        }
    }
}
