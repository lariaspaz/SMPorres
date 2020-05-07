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

namespace SMPorres.Forms.TasasMora
{
    public partial class frmEdición : Lib.AppForms.FormBase
    {
        public frmEdición()
        {
            InitializeComponent();
            this.Text = "Nueva tasa";
            txtTasa.Select();
            dtDesde.Value = Lib.Configuration.CurrentDate;
            dtHasta.Value = dtHasta.Value;
            ckEstado.Checked = true;
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(TasaMora tasa) : this()
        {
            this.Text = "Edición de tasa";
            txtTasa.DecValue = (decimal) tasa.Tasa;
            dtDesde.Value = tasa.Desde;
            dtHasta.Value = tasa.Hasta;
            ckEstado.Checked = tasa.Estado == 1;
        }

        public DateTime Desde
        {
            get
            {
                return dtDesde.Value.Date;
            }
        }

        public DateTime Hasta
        {
            get
            {
                return dtHasta.Value.Date;
            }
        }

        public double Tasa
        {
            get
            {
                return (double)txtTasa.DecValue;
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
                _validator.Validar(txtTasa, txtTasa.DecValue > 0, "No puede tener valor 0 (cero)");
        }        
    }
}
