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
        public frmPagarCuota(int idPago)
        {
            InitializeComponent();

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

            var dp = PagosRepository.ObtenerDetallePago(idPago, dtFechaPago.Value);
            if (dp.Resultado)
            {
                txtImporte.DecValue = dp.ImporteBase;
                txtDescBeca.DecValue = dp.Beca;
                txtDescPagoATérmino.DecValue = dp.DescuentoPagoTérmino;
                txtRecargoPorMora.DecValue = dp.RecargoPorMora;
                txtTotal.DecValue = dp.TotalAPagar;
            }
            else
            {
                ShowError("Falta parametrizar la cuota " + p.NroCuota);
            }

            dtFechaPago.Value = Lib.Configuration.CurrentDate;
            dtFechaPago.Select();

            cbMediosPago.DataSource = MediosPagoRepository.ObtenerMediosPago();
            cbMediosPago.DisplayMember = "Descripcion";
            cbMediosPago.ValueMember = "Id";
            cbMediosPago.SelectedIndex = 0;

            _validator = new FormValidations(this, errorProvider1);
        }

        public int IdMedioPago
        {
            get
            {
                return ((Carrera)cbMediosPago.SelectedItem).Id;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            if (ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            //return _validator.Validar(txtNombre, !String.IsNullOrEmpty(txtNombre.Text.Trim()), "No puede estar vacío") &&
            //    _validator.Validar(txtRecargoPorMora, txtRecargoPorMora.DecValue >= 0, "No puede ser menor que 0");

            return false;
        }
    }
}
