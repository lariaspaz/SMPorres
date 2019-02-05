using SMPorres.Repositories;
using System;
using System.Data;
using System.Linq;
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
            var tds = TiposDocumentoRepository.ObtenerTiposDocumento();
            dgvDatos.SetDataSource(from a in AlumnosRepository.ObtenerAlumnos()
                                   orderby a.Id
                                   select new
                                   {
                                       a.Id,
                                       a.Nombre,
                                       a.Apellido,
                                       TipoDocumento = a.TipoDocumento.Descripcion,
                                       a.NroDocumento,
                                       a.FechaNacimiento,
                                       Activo = a.Estado == (byte)Models.EstadoAlumno.Activo
                                   });
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Models.Alumno ObtenerAlumnoSeleccionado()
        {
            try
            {
                int rowindex = dgvDatos.CurrentCell.RowIndex;
                var id = (Int32)dgvDatos.Rows[rowindex].Cells[0].Value;
                var a = AlumnosRepository.ObtenerAlumnoPorId(id);
                return a;
            }
            catch (Exception)
            {

                return null;
            }
            
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var a = ObtenerAlumnoSeleccionado();
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
            }

            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Nombre";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[2].HeaderText = "Apellido";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[3].HeaderText = "Tip. Doc.";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[4].HeaderText = "Nº Documento";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[4].DefaultCellStyle.Format = "n0";

            dgvDatos.Columns[5].HeaderText = "Fec. Nac.";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvDatos.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvDatos.Columns[6].HeaderText = "Activo";
            dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
                            f.FechaNacimiento, f.Email, f.Dirección, f.Domicilio, f.Estado, f.Sexo);
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
                            f.FechaNacimiento, f.Email, f.Dirección, f.Domicilio, f.Estado, f.Sexo);
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

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            var a = ObtenerAlumnoSeleccionado();
            if (a == null)
            {
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDocumento.Text = "";
                txtFechaNacimiento.Text = "";
                txtEMail.Text = "";
                txtEstado.Text = "";
                txtSexo.Text = "";
                txtDireccion.Text = "";
                txtProvincia.Text = "";
                txtDepartamento.Text = "";
                txtLocalidad.Text = "";
                txtBarrio.Text = "";
                return;
            }
            txtNombre.Text = a.Nombre;
            txtApellido.Text = a.Apellido;
            txtDocumento.Text = String.Format("{0} {1:N0}", a.TipoDocumento.Descripcion, a.NroDocumento);
            txtFechaNacimiento.Text = String.Format("{0:dd/MM/yyyy}", a.FechaNacimiento);
            txtEMail.Text = a.EMail;
            txtEstado.Text = a.LeyendaEstado;
            txtSexo.Text = a.LeyendaSexo;
            txtDireccion.Text = a.Direccion;
            if (a.Domicilio == null)
            {
                txtProvincia.Text = "";
                txtDepartamento.Text = "";
                txtLocalidad.Text = "";
                txtBarrio.Text = "";
            }
            else
            {
                txtProvincia.Text = a.Domicilio.Provincia.Nombre;
                txtDepartamento.Text = a.Domicilio.Departamento.Nombre;
                txtLocalidad.Text = a.Domicilio.Localidad.Nombre;
                txtBarrio.Text = a.Domicilio.Barrio.Nombre;
            }
        }
    }
}
