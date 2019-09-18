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
using SMPorres.Forms.BecasAlumnos;
using System.ServiceModel;

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
                             ca.IdCurso,
                             ca.CicloLectivo,
                             ca.Curso.Nombre,
                             Carrera = ca.Curso.Carrera.Nombre
                         };
            if (!cursos.Any())
            {
                toolTip1.ShowError(this, txtNroDocumento, "El alumno no se inscribió en ningún curso.");
                dgvCursos.DataSource = null;
                dgvPagos.DataSource = null;
                btnGenerarPlanDePago.Enabled = false;
                btnPagarCuota.Enabled = false;
            }
            else
            {
                dgvCursos.SetDataSource(cursos);
                ConsultarPlanesPago();
                btnGenerarPlanDePago.Enabled = true;
                btnPagarCuota.Enabled = true;
            }
        }

        private void ConsultarPlanesPago()
        {
            var query = from pp in PlanesPagoRepository.ObtenerPlanesPago(_alumno.Id, CursoSeleccionado.Id)
                        select new
                        {
                            Id = pp.Id,
                            ProximaCuota = String.Format("{0} de {1}", pp.NroCuota, pp.CantidadCuotas),
                            ImporteCuota = pp.ImporteCuota,
                            PorcentajeBeca = pp.PorcentajeBeca,
                            Estado = pp.LeyendaEstado
                        };
            dgvPlanesPago.SetDataSource(query.ToList());
            if (!query.Any()) dgvPagos.DataSource = null;
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
            foreach (DataGridViewColumn c in dgvCursos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvCursos.Columns[0].HeaderText = "Código";
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvCursos.Columns[1].HeaderText = "Ciclo Lectivo";
            dgvCursos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCursos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvCursos.Columns[2].HeaderText = "Nombre";
            dgvCursos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCursos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvCursos.Columns[3].HeaderText = "Carrera";
            dgvCursos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCursos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private Curso CursoSeleccionado
        {
            get
            {
                if (dgvCursos.CurrentCell == null) return null;
                int rowindex = dgvCursos.CurrentCell.RowIndex;
                var id = (int)dgvCursos.Rows[rowindex].Cells[0].Value;
                var c = CursosRepository.ObtenerCursoPorId(id);
                return c;
            }
        }

        private void dgvPlanesPago_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvPlanesPago.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

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
            using (var f = new PlanesPago.frmEdición(txtNombre.Text, NombreCursoSeleccionado, NombreCurso))
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
            ConsultarPagos();
        }

        private void ConsultarPagos()
        {
            int rowindex = dgvPlanesPago.CurrentCell.RowIndex;
            var id = (Int32)dgvPlanesPago.Rows[rowindex].Cells[0].Value;
            var query = from p in PagosRepository.ObtenerPagos(id)
                        select new
                        {
                            Id = p.Id,
                            Concepto = (p.NroCuota == 0) ? "Matrícula" : String.Format("Cuota Nº {0}", p.NroCuota),
                            FechaVto = p.FechaVto,
                            ImporteCuota = p.ImporteCuota,
                            Fecha = p.Fecha,
                            ImportePagado = p.ImportePagado,
                            MedioPago = p.IdMedioPago.HasValue ? p.MedioPago.Descripcion : null,
                            PorcBeca = p.PorcBeca,
                            //EsContrasiento = p.EsContrasiento == 1,
                            Descripcion = p.Descripcion
                        };
            dgvPagos.SetDataSource(query);
        }

        private void dgvPagos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvPagos.Columns[0].Visible = false;

            dgvPagos.Columns[1].HeaderText = "Concepto";
            dgvPagos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPagos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvPagos.Columns[2].HeaderText = "Vencimiento";
            dgvPagos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPagos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPagos.Columns[2].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPagos.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvPagos.Columns[3].HeaderText = "Importe";
            dgvPagos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPagos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPagos.Columns[3].DefaultCellStyle.Format = "C2";

            dgvPagos.Columns[4].HeaderText = "Fecha Pago";
            dgvPagos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPagos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPagos.Columns[4].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPagos.Columns[5].HeaderText = "Imp. Pagado";
            dgvPagos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPagos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPagos.Columns[5].DefaultCellStyle.Format = "C2";

            dgvPagos.Columns[6].HeaderText = "Medio Pago";
            dgvPagos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPagos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvPagos.Columns[7].HeaderText = "Porc. Beca";
            dgvPagos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPagos.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgvPagos.Columns[7].DefaultCellStyle.Format = "#\\%";

            //dgvPagos.Columns[8].HeaderText = "Contr.";
            //dgvPagos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgvPagos.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvPagos.Columns[8].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPagos.Columns[8].HeaderText = "Descripción";
            dgvPagos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            foreach (DataGridViewColumn c in dgvPagos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private Pago PagoSeleccionado
        {
            get
            {
                tabControl1.SelectedTab = tpCuotas;
                int rowindex = dgvPagos.CurrentCell.RowIndex;
                var id = (int)dgvPagos.Rows[rowindex].Cells[0].Value;
                return PagosRepository.ObtenerPago(id);
            }
        }

        private void btnImprimirCuota_Click(object sender, EventArgs e)
        {
            using (var f = new Pagos.frmInfCupónDePago(PagoSeleccionado.Id)) f.ShowDialog();
        }

        private void dgvCursos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCursos.Focused)
            {
                if (CursoSeleccionado == null)
                {
                    ShowError("El alumno no está asignado a ningún curso.");
                }
                else
                {
                    ConsultarPlanesPago();
                }
            }
        }

        private void btnAnularPlanPago_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea anular el plan de pago?",
                "Anular plan de pago", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    var pp = PlanDePagoSeleccionado;
                    PlanesPagoRepository.AnularPlanDePago(pp.Id);
                    ConsultarDatos();
                    dgvPlanesPago.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == pp.Id);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void btnPagarCuota_Click(object sender, EventArgs e)
        {
            var p = PagoSeleccionado;
            if (p.Fecha != null)
            {
                ShowError("La cuota ya está pagada.");
            }
            else
            {
                using (var f = new Pagos.frmPagarCuota(PagoSeleccionado.Id)) f.ShowDialog();
                ConsultarPagos();
            }
        }

        private PlanPago PlanDePagoSeleccionado
        {
            get
            {
                int rowindex = dgvPlanesPago.CurrentCell.RowIndex;
                var id = (int)dgvPlanesPago.Rows[rowindex].Cells[0].Value;
                return PlanesPagoRepository.ObtenerPlanPagoPorId(id);
            }
        }

        private void btnEditarPlanPago_Click(object sender, EventArgs e)
        {
            var pps = PlanDePagoSeleccionado;
            using (var f = new PlanesPago.frmEdición(txtNombre.Text, NombreCursoSeleccionado, NombreCurso, pps))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var pp = PlanesPagoRepository.ActualizarPorcentajeBeca(pps.Id, f.PorcentajeBeca);
                        ConsultarPlanesPago();
                        dgvPlanesPago.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == pp.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        public string NombreCursoSeleccionado
        {
            get
            {
                int rowindex = dgvCursos.CurrentCell.RowIndex;
                var carrera = (string)dgvCursos.Rows[rowindex].Cells[3].Value;
                return CursoSeleccionado.Nombre + " de " + carrera;
            }
        }

        public string NombreCurso
        {
            get
            {
                int rowindex = dgvCursos.CurrentCell.RowIndex;
                return CursoSeleccionado.Nombre;
            }
        }

        private void AsignarBecaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = PagoSeleccionado;
            var cuota = String.Format("{0} | {1}", p.NroCuota, NombreCursoSeleccionado);
            using (var f = new frmAsignarBeca(cuota))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var beca = BecasAlumnosRepository.Insertar(_alumno.Id, p.Id, f.Beca);
                        ConsultarPagos();
                        dgvPagos.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == p.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n", ex);
                    }
                }
            }
        }

        private void EditarBecaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = PagoSeleccionado;
            var cuota = String.Format("{0} | {1}", p.NroCuota, NombreCursoSeleccionado);
            var beca = p.BecaAlumno;
            using (var f = new frmAsignarBeca(cuota, beca.PorcentajeBeca))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        beca = BecasAlumnosRepository.Actualizar(beca.Id, f.Beca);
                        ConsultarPagos();
                        dgvPagos.SetRow(r => Convert.ToInt32(r.Cells[0].Value) == p.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n", ex);
                    }
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var p = PagoSeleccionado;
            if (p.NroCuota == 0 || p.Fecha.HasValue || Lib.Session.CurrentUser.Id != 1)
            {
                e.Cancel = true;
            }
            else
            {
                AsignarBecaToolStripMenuItem.Visible = p.BecaAlumno == null;
                EditarBecaToolStripMenuItem.Visible = p.BecaAlumno != null;
            }
        }

        private void btnGenerarContraseñaWeb_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro que desea generar una nueva contraseña para el alumno?" +
                        "\nSe reemplazará su contraseña actual.", "Generar contraseña", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string pwdEncriptada = "";
                    var pwd = AlumnosRepository.GenerarContraseña(_alumno.Id, ref pwdEncriptada);
                    string msg = "La contraseña generada para el alumno es:\n" + pwd;
                    //var cliente = new ConsultasWeb.SMPSoapClient();
                    var cliente = CrearCliente();
                    if (cliente.ActualizarPwd(_alumno.Id, pwdEncriptada))
                    {
                        msg += "\nSe actualizó la contraseña del alumno en la web.";
                    }
                    else
                    {
                        msg += "\nNo se pudo actualizar la contraseña del alumno en la web." +
                               "\nSe actualizará cuando se suban los datos de todos los alumnos.";
                    }
                    MessageBox.Show(msg, "Generar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al intentar generar la contraseña del alumno:\n", ex);
            }
        }

        private static ConsultasWeb.SMPSoapClient CrearCliente()
        {
            //Specify the binding to be used for the client.
            BasicHttpBinding binding = new BasicHttpBinding();

            //Specify the address to be used for the client.
            EndpointAddress address =
               new EndpointAddress(Repositories.ConfiguracionRepository.ObtenerConfiguracion().EndpointAddress);

            return new ConsultasWeb.SMPSoapClient(binding, address);
        }
    }
}
