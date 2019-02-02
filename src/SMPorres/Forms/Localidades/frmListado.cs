using CustomLibrary.Extensions.Controls;
using SMPorres.Lib.AppForms;
using SMPorres.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SMPorres.Forms.Localidades
{
    public partial class frmListado : Lib.AppForms.FormBase
    {
        public int IdProvincia
        {
            get
            {
                return Convert.ToInt32(cbProvincias.SelectedValue);
            }
        }

        public int IdDepartamento
        {
            get
            {
                return Convert.ToInt32(cbDepartamentos.SelectedValue);
            }
        }

        public frmListado()
        {
            InitializeComponent();
            cbProvincias.DataSource = ProvinciasRepository.ObtenerProvincias();
            cbProvincias.ValueMember = "Id";
            cbProvincias.DisplayMember = "Nombre";
            Action<ComboBox, string> seleccionar = (cb, s) =>
            {
                for (int i = 0; i < cb.Items.Count; i++)
                {
                    if (cb.GetItemText(cb.Items[i]).Contains(s))
                    {
                        cb.SelectedIndex = i;
                        break;
                    }
                }
            };
            seleccionar(cbProvincias, "Santiago");
            CargarDepartamentos(IdProvincia);
            seleccionar(cbDepartamentos, "Capital");
            ConsultarDatos();
        }

        private void CargarDepartamentos(int idProvincia)
        {
            var d = DepartamentosRepository.ObtenerDepartamentosPorProvincia(idProvincia);
            cbDepartamentos.DataSource = d;
            cbDepartamentos.DisplayMember = "Nombre";
            cbDepartamentos.ValueMember = "Id";
            if (d.Any()) cbDepartamentos.SelectedIndex = 0;
        }

        private void ConsultarDatos()
        {
            var query = LocalidadesRepository.ObtenerLocalidadesPorDepartamento(IdDepartamento);
            dgvDatos.SetDataSource(from d in query select new { d.Id, d.Nombre });
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

            dgvDatos.Columns[1].HeaderText = "Nombre";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            using (var f = new frmInputQuery("Nueva localidad", "Nueva localidad de " + cbDepartamentos.Text + ":"))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var d = LocalidadesRepository.Insertar(IdDepartamento, f.Descripción.Trim());
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == d.Id);
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
            var loc = ObtenerLocalidadSeleccionada();
            using (var f = new frmInputQuery("Edición de departamento", "Departamento de " + 
                cbProvincias.Text + ":", loc.Nombre))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LocalidadesRepository.Actualizar(loc.Id, f.Descripción.Trim());
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == loc.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private Models.Localidad ObtenerLocalidadSeleccionada()
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;            
            return LocalidadesRepository.ObtenerLocalidadPorId(id);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Models.Localidad m = ObtenerLocalidadSeleccionada();
            if (MessageBox.Show("¿Está seguro de que desea eliminar la localidad seleccionada?",
                "Eliminar localidad", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    LocalidadesRepository.Eliminar(m.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void cbProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDepartamentos(IdProvincia);
            ConsultarDatos();
        }

        private void cbDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarDatos();
        }
    }
}
