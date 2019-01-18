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

namespace SMPorres.Forms
{
    public partial class frmConfiguración : Lib.AppForms.FormBase
    {
        private FormValidations _validator;

        public frmConfiguración()
        {
            InitializeComponent();
            var conf = ConfiguracionRepository.ObtenerConfiguracion();
            txtDescPagoTérmino.DecValue =(decimal) conf.DescuentoPagoTermino;
            txtInterésPorMora.DecValue = (decimal) conf.InteresPorMora;
            txtCicloLectivo.IntValue = conf.CicloLectivo;
            this.Text = "Configuración general";
            txtDescPagoTérmino.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public double DescuentoPagoATérmino
        {
            get
            {
                return (double)txtDescPagoTérmino.DecValue;
            }
        }

        public double InterésPorMora
        {
            get
            {
                return (double)txtInterésPorMora.DecValue;
            }
        }

        public short CicloLectivo
        {
            get
            {
                return (short) txtCicloLectivo.IntValue;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                try
                {
                    ConfiguracionRepository.Actualizar(DescuentoPagoATérmino, InterésPorMora, CicloLectivo);
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                }
            }
        }

        private bool ValidarDatos()
        {
            return
                _validator.Validar(txtDescPagoTérmino, DescuentoPagoATérmino >= 0, "No puede ser menor que cero") &&
                _validator.Validar(txtInterésPorMora, InterésPorMora >= 0, "No puede ser menor que cero") &&
                _validator.Validar(txtCicloLectivo, CicloLectivo >= 2019, "No puede ser menor que 2019");
        }
    }
}
