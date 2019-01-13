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
    public partial class frmEdición : Form
    {
        private FormValidations _validator;

        public frmEdición()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva carrera";
            txtNombre.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(Carrera carrera) : this()
        {
            this.Text = "Edición de carrera";
            txtNombre.Text = carrera.Nombre;
            txtDuración.Text = carrera.Duracion.ToString();
            txtImporte1Vto.Text = String.Format("{0:n}", carrera.Importe1Vto);
            txtImporte2Vto.Text = String.Format("{0:n}", carrera.Importe2Vto);
            txtImporte3Vto.Text = String.Format("{0:n}", carrera.Importe3Vto);
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
                return (short)txtImporte1Vto.IntValue;
            }
        }

        public decimal Importe1Vto
        {
            get
            {
                return txtImporte1Vto.DecValue;
            }
        }

        public decimal Importe2Vto
        {
            get
            {
                return txtImporte2Vto.DecValue;
            }
        }

        public decimal Importe3Vto
        {
            get
            {
                return txtImporte3Vto.DecValue;
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
                _validator.Validar(txtDuración, Duración > 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtImporte1Vto, Importe1Vto > 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtImporte2Vto, Importe2Vto > 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtImporte3Vto, Importe3Vto > 0, "No puede ser menor o igual que cero");
        }        
    }
}
