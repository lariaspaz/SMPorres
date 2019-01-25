using CustomLibrary.Extensions.Controls;
using CustomLibrary.Lib.Extensions;
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
    public partial class frmPanelAlumno : FormBase
    {
        private FormValidations _validator;

        public frmPanelAlumno()
        {
            InitializeComponent();
            _validator = new FormValidations(this, errorProvider1);
        }

        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            using (var f = new frmBuscarAlumnos())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    txtNroDocumento.DecValue = f.AlumnoSeleccionado.NroDocumento;
                    ConsultarDatos();
                }
            }
        }

        private void txtNroDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConsultarDatos();
            }
        }

        private void ConsultarDatos()
        {
            var a = AlumnosRepository.BuscarAlumnoPorNroDocumento(txtNroDocumento.DecValue);
            if (!_validator.Validar(txtNroDocumento, a != null, "No existe el alumno"))
            {
                return;
            }
            txtNombre.Text = a.Apellido + ", " + a.Nombre;

            var cursos = from ca in CursosAlumnosRepository.ObtenerCursosPorAlumno(a.Id)
                         orderby ca.Id
                         select new
                         {
                             ca.Id,
                             ca.Nombre,
                             Carrera = ca.Carrera.Nombre
                         };
            if (!cursos.Any())
            {
                toolTip1.ShowError(this, txtNroDocumento, "El alumno no se inscribió en ningún curso.");
                dgvCursos.DataSource = null;
                btnGenerarPlanPago.Enabled = false;
            }
            else
            {
                dgvCursos.SetDataSource(cursos);
                dgvPlanesPago.SetDataSource(PlanesPagoRepository.ObtenerPlanesPagoPorAlumnoYCurso(a.Id, CursoSeleccionado.Id));
                btnGenerarPlanPago.Enabled = true;
            }
        }

        private void frmPanelAlumno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditar.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnEliminar.PerformClick();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            ConsultarDatos();
        }

        private void dgvCursos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewColumn c in dgvCursos.Columns)
            //{
            //    c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}

            dgvCursos.Columns[0].HeaderText = "Código";
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvCursos.Columns[1].HeaderText = "Nombre";
            dgvCursos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCursos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvCursos.Columns[2].HeaderText = "Carrera";
            dgvCursos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCursos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private Models.Curso CursoSeleccionado
        {
            get
            {
                int rowindex = dgvCursos.CurrentCell.RowIndex;
                var id = (int)dgvCursos.Rows[rowindex].Cells[0].Value;
                var c = CursosRepository.ObtenerCursoPorId(id);
                return c;
            }
        }

        private void btnGenerarPlanPago_Click(object sender, EventArgs e)
        {
            string curso = CursoSeleccionado.Nombre + " de " + CursoSeleccionado.Carrera;
            using (var f = new PlanesPago.frmEdición(txtNombre.Text, curso))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //var c = PlanesPagoRepository.Insertar();
                        //ConsultarDatos();
                        //dgvDatos.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == c.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void dgvPlanesPago_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }
    }
}
