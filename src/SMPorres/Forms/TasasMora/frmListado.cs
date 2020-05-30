﻿using CustomLibrary.Extensions.Controls;
using SMPorres.Repositories;
using System;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;

namespace SMPorres.Forms.TasasMora
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
            dgvDatos.SetDataSource(from t
                                   in TasasMoraRepository.ObtenerTasas()
                                   select new
                                   {
                                       t.Id,
                                       t.Tasa,
                                       t.Desde,
                                       t.Hasta,
                                       t.LeyendaEstado
                                   });
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDatos.Columns[0].HeaderText = "Código";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Tasa Mensual (%)";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[1].DefaultCellStyle.Format = "0.0";

            dgvDatos.Columns[2].HeaderText = "Vigencia Desde";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[2].DefaultCellStyle.Format = "ddd, dd/MM/yyyy";

            dgvDatos.Columns[3].HeaderText = "Vigencia Hasta";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDatos.Columns[3].DefaultCellStyle.Format = "ddd, dd/MM/yyyy";

            dgvDatos.Columns[4].HeaderText = "Estado";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
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
                        var c = TasasMoraRepository.Insertar(f.Tasa, f.Desde, f.Hasta);
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
            Models.TasaMora t = ObtenerTasaSeleccionada();
            using (var f = new frmEdición(t))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        TasasMoraRepository.Actualizar(t.Id, f.Tasa, f.Desde, f.Hasta, f.Estado);
                        ConsultarDatos();
                        dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == t.Id);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private Models.TasaMora ObtenerTasaSeleccionada()
        {
            try
            {
                int rowindex = dgvDatos.CurrentCell.RowIndex;
                var id = (int)dgvDatos.Rows[rowindex].Cells[0].Value;
                var t = TasasMoraRepository.ObtenerTasaPorId(id);
                return t;
            }
            catch (Exception)
            {
                return null;
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var m = ObtenerTasaSeleccionada();
            if (MessageBox.Show("¿Está seguro de que desea eliminar la tasa seleccionada?",
                "Eliminar tasa", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    TasasMoraRepository.Eliminar(m.Id);
                    ConsultarDatos();
                    dgvDatos.SetRow(r => Convert.ToDecimal(r.Cells[0].Value) == m.Id);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void frmListado_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "";
            switch (TasasMoraRepository.ValidarTasas())
            {
                default:
                case TasasMoraRepository.ValidarTasasResult.Ok:
                    if (!ActualizarWeb())
                        e.Cancel = true;
                    return;
                case TasasMoraRepository.ValidarTasasResult.HayRangosNoDefinidos:
                    msg = "Hay rangos de fechas sin definir.";
                    break;
                case TasasMoraRepository.ValidarTasasResult.NoHayRangoPara2019:
                    msg = "No hay un rango de fechas para el año 2019.";
                    break;
                case TasasMoraRepository.ValidarTasasResult.NoHayRangoParaHoy:
                    msg = "No hay un rango de fechas que contenga al día de hoy.";
                    break;
            }
            if (MessageBox.Show("¿Está seguro que desea cerrar el formulario?\n" + msg,
                "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }

            if (!ActualizarWeb())
                e.Cancel = true;
        }

        private bool ActualizarWeb()
        {
            var binding = new BasicHttpBinding();
            var address = new EndpointAddress(ConfiguracionRepository.ObtenerConfiguracion().EndpointAddress);
            var cliente = new ConsultasWeb.SMPSoapClient(binding, address);
            try
            {
                cliente.ActualizarTasasMora(WebRepository.ObtenerTasasMora().ToArray());
                return true;
            }
            catch (Exception ex)
            {
                var msg = "No se pudieron actualizar las tasas en la web: \n" + ex.Message;
                msg += "\n\n" + "¿Desea cerrar de todos modos el formulario?";
                return (MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Error) == DialogResult.Yes);
            }
            finally
            {
                cliente.Close();
            }
        }
    }
}
