using CustomLibrary.Extensions.Controls;
using SMPorres.Lib.AppForms;
using SMPorres.Lib.Validations;
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

namespace SMPorres.Forms.Alumnos
{
    public partial class frmBuscarAlumnos : FormBase
    {
        public frmBuscarAlumnos()
        {
            InitializeComponent();
            cbTipo.SelectedIndex = 0;
            dgvDatos.DataSource = null;
            _validator = new FormValidations(this, errorProvider1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            dgvDatos.DataSource = null;
            IList<Models.Alumno> result;
            if (cbTipo.SelectedIndex == 0)
            {
                result = AlumnosRepository.BuscarAlumnosPorDocumento(txtDato.Text);
            }
            else
            {
                result = AlumnosRepository.BuscarAlumnosPorNombre(txtDato.Text);
            }
            dgvDatos.SetDataSource(result.Select(r => new { r.Id, r.NroDocumento, r.Nombre }));
            if (result.Any())
            {
                dgvDatos.Focus();
            }
        }

        private void frmBuscarAlumnos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDato.Focused)
                {
                    btnBuscar.PerformClick();
                }
                else if (dgvDatos.Focused)
                {
                    e.Handled = true;
                    DialogResult = DialogResult.OK;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnSalir.PerformClick();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public Models.Alumno AlumnoSeleccionado
        {
            get
            {
                int rowindex = dgvDatos.SelectedCells[0].RowIndex;
                var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
                return AlumnosRepository.ObtenerAlumnoPorId(id);
            }
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
            dgvDatos.Columns[0].Visible = false;

            dgvDatos.Columns[1].HeaderText = "Documento";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[1].DefaultCellStyle.Format = "n0";

            dgvDatos.Columns[2].HeaderText = "Nombre";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private bool ValidarDatos()
        {
            return _validator.Validar(txtDato, !String.IsNullOrEmpty(txtDato.Text.Trim()), "No puede estar vacío");
        }
    }
}
