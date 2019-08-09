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

namespace SMPorres.Forms.PlanesPago
{
    public partial class frmEdición : FormBase
    {
        public frmEdición(string alumno, string curso)
        {
            InitializeComponent();
            this.Text = "Nuevo plan de pago";
            txtAlumno.Text = alumno;
            txtCurso.Text = curso;
            txtCuota.Text = String.Format("1 de 9", ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo);
            txtPorcentajeBeca.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(string alumno, string curso, PlanPago plan) : this(alumno, curso)
        {
            this.Text = "Edición de plan de pago";
            txtCuota.Text = String.Format("{0} de {1}", plan.NroCuota, plan.CantidadCuotas);
            txtPorcentajeBeca.DecValue = plan.PorcentajeBeca;
        }

        public short PorcentajeBeca
        {
            get
            {
                return (short) txtPorcentajeBeca.IntValue;
            }
        }

        public short MinCuota
        {
            get
            {
                return (short)txtMinCuota.IntValue;
            }
        }

        public short MaxCuota
        {
            get
            {
                return (short)txtMaxCuota.IntValue;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            return _validator.Validar(txtPorcentajeBeca, txtPorcentajeBeca.DecValue >= 0, "No puede ser menor que 0") &&
                _validator.Validar(txtMinCuota, txtMinCuota.DecValue >= 0, "No puede ser menor que 0") &&
                _validator.Validar(txtMaxCuota, txtMaxCuota.DecValue >= 0, "No puede ser menor que 0") &&
                _validator.Validar(txtMinCuota, txtMinCuota.DecValue <= txtMaxCuota.DecValue, "No puede ser mayor que la Cuota Final") &&
                _validator.Validar(txtMaxCuota, txtMaxCuota.DecValue <= 9, "No puede ser mayor que 9");
                //_validator.Validar(txtApellido, !String.IsNullOrEmpty(Apellido), "No puede estar vacío") &&
                //_validator.Validar(txtNroDocumento, NroDocumento > 0, "No puede ser menor o igual que cero");
        }        
    }
}
