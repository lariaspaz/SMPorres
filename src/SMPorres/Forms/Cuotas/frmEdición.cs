using SMPorres.Lib.AppForms;
using SMPorres.Lib.Validations;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Cuotas
{
    public partial class frmEdición : FormBase
    {
        public frmEdición()
        {
            InitializeComponent();
            this.Text = "Nueva cuota";
            txtNroCuota.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(Cuota cuota) : this()
        {
            this.Text = "Edición de cuota";
            txtNroCuota.Text = cuota.NroCuota.ToString();
            dtVtoCuota.Value = cuota.VtoCuota;
            txtPeríodo.Text = cuota.CicloLectivo.ToString();
        }

        public short NroCuota
        {
            get
            {
                return (short)txtNroCuota.IntValue;
            }
        }

        public DateTime VtoCuota
        {
            get
            {
                return dtVtoCuota.Value.Date;
            }
        }

        public short CicloLectivo
        {
            get
            {
                return (short) txtPeríodo.IntValue;
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
            return
                _validator.Validar(txtNroCuota, NroCuota >= 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtPeríodo, VtoCuota.Year == CicloLectivo, "El vencimiento no coincide pertenece al ciclo lectivo ingresado.");

        }
    }
}
