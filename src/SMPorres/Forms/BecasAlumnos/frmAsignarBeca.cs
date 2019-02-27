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

namespace SMPorres.Forms.BecasAlumnos
{
    public partial class frmAsignarBeca : Lib.AppForms.FormBase
    {
        public frmAsignarBeca(string cuota)
        {
            InitializeComponent();
            this.Text = "Nueva beca";
            txtCuota.Text = cuota;
            txtBeca.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmAsignarBeca(string cuota, double? beca) : this(cuota)
        {
            this.Text = "Edición de beca";
            txtBeca.IntValue = (int)beca;
        }

        public short Beca
        {
            get
            {
                return (short)txtBeca.IntValue;
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
            return _validator.Validar(txtBeca, Beca > 0, "No puede ser menor o igual que cero");
        }
    }
}
