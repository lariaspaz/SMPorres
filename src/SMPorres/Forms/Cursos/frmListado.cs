using CustomLibrary.Extensions.Controls;
using SMPorres.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SMPorres.Forms.Cursos
{
    public partial class frmListado : Lib.AppForms.FormBase
    {
        public frmListado()
        {
            InitializeComponent();
            cbCarreras.DataSource = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
        }

        private int IdCarrera
        {
            get
            {
                return ((Models.Carrera)cbCarreras.SelectedItem).Id;
            }
        }

        private void ConsultarDatos()
        {
            var qry = from c in CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera)
                      orderby c.Id
                      select new
                      {
                          c.Id,
                          c.Nombre,
                          c.IdCarrera,
                          c.ImporteMatricula,
                          c.ImporteCuota,
                          c.Modalidad
                      };
            dgvDatos.SetDataSource(qry.ToList());
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

            dgvDatos.Columns[2].Visible = false;

            dgvDatos.Columns[3].HeaderText = "Imp. Matr.";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[3].DefaultCellStyle.Format = "C2";

            dgvDatos.Columns[4].HeaderText = "Imp. Cta.";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[4].DefaultCellStyle.Format = "C2";

            dgvDatos.Columns[5].HeaderText = "Modalidad";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //dgvDatos.Columns[5].DefaultCellStyle.Format = "C2";
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
                        var c = CursosRepository.Insertar(f.Nombre, f.IdCarrera, f.ImporteMatrícula, f.ImporteCuota, f.Modalidad);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == c.Id);
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
            Models.Curso c = ObtenerCursoSeleccionado();
            using (var f = new frmEdición(c))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        CursosRepository.Actualizar(c.Id, f.Nombre, f.IdCarrera, f.ImporteMatrícula, f.ImporteCuota, f.Modalidad);
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

        private Models.Curso ObtenerCursoSeleccionado()
        {
            try
            {
                int rowindex = dgvDatos.CurrentCell.RowIndex;
                var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
                var c = CursosRepository.ObtenerCursoPorId(id);
                return c;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var c = ObtenerCursoSeleccionado();
            if (MessageBox.Show("¿Está seguro de que desea eliminar el curso seleccionado?",
                "Eliminar curso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    CursosRepository.Eliminar(c.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == c.Id);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            ConsultarDatos();
        }
    }
}
