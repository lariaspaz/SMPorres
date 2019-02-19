using CustomLibrary.Extensions.Controls;
using SMPorres.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SMPorres.Forms.Cuotas
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
            dgvDatos.SetDataSource(CuotasRepository.ObtenerCuotas());
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Nº Cuota";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[2].HeaderText = "Vto. Cuota";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
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
                        var c = CuotasRepository.Insertar(f.NroCuota, f.VtoCuota);
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
            var c = ObtenerCuotaSeleccionada();
            using (var f = new frmEdición(c))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        CuotasRepository.Actualizar(c.Id, f.NroCuota, f.VtoCuota);
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

        private Models.Cuota ObtenerCuotaSeleccionada()
        {
            try
            {
                int rowindex = dgvDatos.CurrentCell.RowIndex;
                var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
                var c = CuotasRepository.ObtenerCuotaPorId(id);
                return c;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Models.Cuota m = ObtenerCuotaSeleccionada();
            if (MessageBox.Show("¿Está seguro de que desea eliminar la cuota seleccionada?",
                "Eliminar cuota", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    CuotasRepository.Eliminar(m.Id);
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
