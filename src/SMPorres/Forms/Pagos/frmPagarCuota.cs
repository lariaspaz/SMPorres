using SMPorres.Lib.AppForms;
using SMPorres.Lib.Validations;
using SMPorres.Models;
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

namespace SMPorres.Forms.Pagos
{
    public partial class frmPagarCuota : FormBase
    {
        private int _idPago;
        private Pago _pago;

        public frmPagarCuota(int idPago)
        {
            InitializeComponent();

            _idPago = idPago;

            var p = PagosRepository.ObtenerPago(idPago);
            txtCurso.Text = String.Format("{0} de {1}", p.PlanPago.Curso.Nombre, p.PlanPago.Curso.Carrera.Nombre);

            if (p.NroCuota == 0)
            {
                txtCuota.Text = "Matrícula";
                txtCuota.TextAlign = HorizontalAlignment.Left;
            }
            else
            {
                txtCuota.Text = String.Format("{0} de {1}", p.NroCuota, Lib.Configuration.MaxCuotas);
                txtCuota.TextAlign = HorizontalAlignment.Right;
            }

            CalcularDetalle();

            dtFechaPago.Value = Lib.Configuration.CurrentDate;
            dtFechaPago.Select();

            cbMediosPago.DataSource = MediosPagoRepository.ObtenerMediosPago();
            cbMediosPago.DisplayMember = "Descripcion";
            cbMediosPago.ValueMember = "Id";
            cbMediosPago.SelectedIndex = 0;

            _validator = new FormValidations(this, errorProvider1);
        }

        private void CalcularDetalle()
        {
            _pago = PagosRepository.ObtenerDetallePago(_idPago, dtFechaPago.Value.Date);
            if (_pago != null)
            {
                txtImporte.DecValue = _pago.ImporteCuota;
                txtDescBeca.DecValue = _pago.ImporteBeca.Value;
                txtDescPagoATérmino.DecValue = _pago.ImportePagoTermino.Value;
                txtRecargoPorMora.DecValue = _pago.ImporteRecargo.Value;
                txtTotal.DecValue = _pago.ImportePagado.Value;
                txtFechaVto.Text = _pago.FechaVto.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                ShowError("Falta parametrizar la cuota " + _pago.NroCuota);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea pagar esta cuota?", "Confirme el pago",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _pago.Fecha = dtFechaPago.Value.Date;
                    _pago.IdMedioPago = (int)cbMediosPago.SelectedValue;
                    _pago.Descripcion = txtDescripcion.Text.Trim();
                    PagosRepository.PagarCuota(_pago);
                }
                catch (Exception ex)
                {
                    ShowError("No se pudo grabar el pago:\n", ex);
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            //return _validator.Validar(txtNombre, !String.IsNullOrEmpty(txtNombre.Text.Trim()), "No puede estar vacío") &&
            //    _validator.Validar(txtRecargoPorMora, txtRecargoPorMora.DecValue >= 0, "No puede ser menor que 0");

            return false;
        }

        private void dtFechaPago_ValueChanged(object sender, EventArgs e)
        {
            CalcularDetalle();
        }
    }
}
