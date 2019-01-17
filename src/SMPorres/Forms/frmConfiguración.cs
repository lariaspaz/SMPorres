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
            txtDescPagoTérmino.Text = conf.DescuentoPagoTermino.ToString();
            txtInterésPorMora.Text = conf.InteresPorMora.ToString();
            this.Text = "Configuración general";
            txtDescPagoTérmino.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public double DescuentoPagoATérmino
        {
            get
            {
                return (double) txtDescPagoTérmino.DecValue;
            }
        }

        public double InterésPorMora
        {
            get
            {
                return (double) txtInterésPorMora.DecValue;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                try
                {
                    ConfiguracionRepository.Actualizar(DescuentoPagoATérmino, InterésPorMora);
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
                _validator.Validar(txtInterésPorMora, InterésPorMora >= 0, "No puede ser menor que cero");
        }        
    }
}
