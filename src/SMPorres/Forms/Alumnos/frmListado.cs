using CustomLibrary.Extensions.Controls;
using SMPorres.Prints;
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
    public partial class frmListado : Lib.AppForms.FormBase
    {
        public frmListado()
        {
            InitializeComponent();
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            dgvDatos.SetDataSource(from a in AlumnosRepository.ObtenerAlumnos()
                                   orderby a.Id
                                   select new
                                   {
                                       a.Id,
                                       a.Nombre,
                                       a.Apellido,
                                       a.IdTipoDocumento,
                                       a.NroDocumento,
                                       FechaNacimiento = String.Format("{0:dd/mm/yyyy}", a.FechaNacimiento),
                                       a.EMail,
                                       a.Direccion,
                                       a.IdDomicilio,
                                       a.Estado,
                                       a.Sexo
                                   });
        }

        private void ConsultarDireccionEMail(int IdDomicilio)
        {
            var d = new Models.Domicilio();
            d = DomiciliosRepository.ObtenerDomicilioPorId(IdDomicilio);

            txtProvincia.Text = DomiciliosRepository.ObtenerProvincia(d.IdProvincia);
            txtDepartamento.Text = DomiciliosRepository.ObtenerDepartamento(d.IdDepartamento);
            txtLocalidad.Text = DomiciliosRepository.ObtenerLocalidad(d.IdLocalidad);
            txtBarrio.Text = DomiciliosRepository.ObtenerBarrio(d.IdBarrio);

            int rowindex = dgvDatos.CurrentCell.RowIndex;
            txtDireccion.Text = (string)dgvDatos.Rows[rowindex].Cells[7].Value;
            txtEMail.Text = (string)dgvDatos.Rows[rowindex].Cells[6].Value;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Models.Alumno ObtenerAlumnoSeleccionado()
        {
            int rowindex = dgvDatos.CurrentCell.RowIndex;
            var id = (Int32)dgvDatos.Rows[rowindex].Cells[0].Value;
            var a = AlumnosRepository.ObtenerAlumnoPorId(id);
            return a;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Models.Alumno a = ObtenerAlumnoSeleccionado();
            if (AlumnosRepository.CursoAsignado(a.Id))
            {
                MessageBox.Show("No puede eliminar el alumno, porque está asigado a un curso...",
                    "Atención", MessageBoxButtons.OK);
                return;
            }
            
            if (MessageBox.Show("¿Está seguro de que desea eliminar el alumno seleccionado?",
                "Eliminar alumnos", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    AlumnosRepository.Eliminar(a.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == a.Id);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void frmListado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
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

            dgvDatos.Columns[2].HeaderText = "Apellido";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvDatos.Columns[2].DefaultCellStyle.Format = "d";

            dgvDatos.Columns[3].HeaderText = "Doc.";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[4].HeaderText = "Número";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[5].HeaderText = "Fecha Nac.";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            //dgvDatos.Columns[6].HeaderText = "Email";
            //dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgvDatos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[6].Visible = false;
            dgvDatos.Columns[7].Visible = false;
            dgvDatos.Columns[8].Visible = false;
            dgvDatos.Columns[9].Visible = false;

            dgvDatos.Columns[10].HeaderText = "Sexo";
            dgvDatos.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var f = new frmEdición())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var a = AlumnosRepository.Insertar(f.Nombre, f.Apellido, f.IdTipoDocumento, f.NroDocumento,
                            f.FechaNacimiento, f.Email, f.Dirección, f.IdDomicilio, f.Estado, f.Sexo);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == a.Id);
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
            Models.Alumno a = ObtenerAlumnoSeleccionado();
            using (var f = new frmEdición(a))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AlumnosRepository.Actualizar(a.Id, f.Nombre, f.Apellido, f.IdTipoDocumento, f.NroDocumento,
                            f.FechaNacimiento, f.Email, f.Dirección, f.IdDomicilio, f.Estado, f.Sexo);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == a.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProvincia.Text = "";
            txtDepartamento.Text = "";
            txtLocalidad.Text = "";
            txtBarrio.Text = "";
            txtEMail.Text = "";

            int rowindex = dgvDatos.CurrentCell.RowIndex;
            ConsultarDireccionEMail((Int32)dgvDatos.Rows[rowindex].Cells[8].Value);
        }

        private void bntPrint_Click(object sender, EventArgs e)
        {
            using (var f = new ListadoAlumnosXCurso()) f.ShowDialog();
        }
    }
}
