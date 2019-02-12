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

namespace SMPorres.Forms.Carreras
{
    public partial class frmEdición : Lib.AppForms.FormBase
    {
        public frmEdición()
        {
            InitializeComponent();
            this.Text = "Nueva carrera";
            txtNombre.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(Carrera carrera) : this()
        {
            this.Text = "Edición de carrera";
            txtNombre.Text = carrera.Nombre;
            txtDuración.Text = carrera.Duracion.ToString();
            txtImporte.Text = String.Format("{0:n}", carrera.Importe);
            ckEstado.Checked = carrera.Estado == 1;
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
        }

        public short Duración
        {
            get
            {
                return (short)txtDuración.IntValue;
            }
        }

        public decimal Importe
        {
            get
            {
                return txtImporte.DecValue;
            }
        }

        public short Estado
        {
            get
            {
                return (short)(ckEstado.Checked ? 1 : 0);
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
                _validator.Validar(txtNombre, !String.IsNullOrEmpty(Nombre.Trim()), "No puede estar vacío") &&
                _validator.Validar(txtDuración, Duración > 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtImporte, Importe > 0, "No puede ser menor o igual que cero");
        }        
    }
}
