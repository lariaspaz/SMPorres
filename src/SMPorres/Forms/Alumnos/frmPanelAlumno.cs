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
using SMPorres.Models;

namespace SMPorres.Forms.Alumnos
{
    public partial class frmPanelAlumno : FormBase
    {
        private Alumno _alumno;

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
            _alumno = AlumnosRepository.BuscarAlumnoPorNroDocumento(txtNroDocumento.DecValue);
            if (!_validator.Validar(txtNroDocumento, _alumno != null, "No existe el alumno"))
            {
                return;
            }
            txtNombre.Text = _alumno.Apellido + ", " + _alumno.Nombre;

            var cursos = from ca in CursosAlumnosRepository.ObtenerCursosPorAlumno(_alumno.Id)
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
                GenerarPlanDePagoToolStripMenuItem.Enabled = false;
                CobrarCuotaToolStripMenuItem.Enabled = false;
            }
            else
            {
                dgvCursos.SetDataSource(cursos);
                ConsultarPlanesPago();
                GenerarPlanDePagoToolStripMenuItem.Enabled = true;
                CobrarCuotaToolStripMenuItem.Enabled = true;
            }
        }

        private void ConsultarPlanesPago()
        {
            var query = from pp in PlanesPagoRepository.ObtenerPlanesPago(_alumno.Id, CursoSeleccionado.Id)
                        select new {
                            Id = pp.Id,
                            ProximaCuota = String.Format("{0} de {1}", pp.NroCuota, pp.CantidadCuotas),
                            ImporteCuota = pp.ImporteCuota,
                            PorcentajeBeca = pp.PorcentajeBeca,
                            Estado = pp.LeyendaEstado
                        };
            dgvPlanesPago.SetDataSource(query.ToList());
        }

        private void frmPanelAlumno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnSalir.PerformClick();
            //else if (e.Control && e.KeyCode == Keys.N) btnNuevo.PerformClick();
            else if (e.Control && e.KeyCode == Keys.F4) btnEditarPlanPago.PerformClick();
            else if (e.Control && e.KeyCode == Keys.Delete) btnAnularPlanPago.PerformClick();
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

        private string CarreraSeleccionada
        {
            get
            {
                int rowindex = dgvCursos.CurrentCell.RowIndex;
                return (string)dgvCursos.Rows[rowindex].Cells[2].Value;
            }
        }

        private void dgvPlanesPago_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //CantidadCuotas = pp.CantidadCuotas,
            //NroCuota = pp.NroCuota,
            //ImporteCuota = pp.ImporteCuota,
            //PorcentajeBeca = pp.PorcentajeBeca,
            //Estado = pp.Estado,
            //FechaGrabacion = pp.FechaGrabacion

            //foreach (DataGridViewColumn c in dgvCursos.Columns)
            //{
            //    c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}

            dgvPlanesPago.Columns[0].HeaderText = "Código";
            dgvPlanesPago.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPlanesPago.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvPlanesPago.Columns[1].HeaderText = "Próx. Cuota";
            dgvPlanesPago.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlanesPago.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvPlanesPago.Columns[2].HeaderText = "Importe Cuota";
            dgvPlanesPago.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlanesPago.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvPlanesPago.Columns[2].DefaultCellStyle.Format = "C2";

            dgvPlanesPago.Columns[3].HeaderText = "Porc. Beca";
            dgvPlanesPago.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlanesPago.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvPlanesPago.Columns[3].DefaultCellStyle.Format = "0\\%";

            dgvPlanesPago.Columns[4].HeaderText = "Estado";
            dgvPlanesPago.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPlanesPago.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void GenerarPlanDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curso = CursoSeleccionado.Nombre + " de " + CarreraSeleccionada;
            using (var f = new PlanesPago.frmEdición(txtNombre.Text, curso))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var c = PlanesPagoRepository.Insertar(_alumno.Id, CursoSeleccionado.Id, f.PorcentajeBeca);
                        ConsultarPlanesPago();
                        dgvPlanesPago.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == c.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void dgvPlanesPago_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = dgvPlanesPago.CurrentCell.RowIndex;
            var id = (Int32)dgvPlanesPago.Rows[rowindex].Cells[0].Value;
            var query = from p in PagosRepository.ObtenerPagos(id)
                        select new
                        {
                            IdPago = p.IdPago,
                            Concepto = (p.NroCuota == 0)? "Matrícula":String.Format("Cuota Nº {0}", p.NroCuota),
                            Importe = p.ImporteCuota,
                            Fecha = p.Fecha
                        };
            dgvPagos.SetDataSource(query);
        }

        private void dgvPagos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvCursos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvPagos.Columns[0].Visible = false;

            dgvPagos.Columns[1].HeaderText = "Concepto";
            dgvPagos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPagos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvPagos.Columns[2].HeaderText = "Importe";
            dgvPagos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPagos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPagos.Columns[2].DefaultCellStyle.Format = "C2";

            dgvPagos.Columns[3].HeaderText = "Fecha";
            dgvPagos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPagos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void btnImprimirCuota_Click(object sender, EventArgs e)
        {

        }
    }
}
