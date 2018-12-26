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
        public frmEdición()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //txtDescripción.Select();
            this.Text = "Nueva transacción";
            //cbRubros.DataSource = RubrosRepository.ObtenerRubros();
            //cbRubros.ValueMember = "Id";
            //cbRubros.DisplayMember = "Descripcion";
            //cbRubros.SelectedValue = idRubro;
        }

        public frmEdición(Carrera carrera) : this()
        {
            this.Text = "Edición de transacción";
            //cbRubros.DataSource = RubrosRepository.ObtenerRubros();
            //cbRubros.ValueMember = "Id";
            //cbRubros.DisplayMember = "Descripcion";
            //cbRubros.SelectedValue = transacción.IdRubro;
            //txtDescripción.Text = transacción.Descripcion;
            //ckEsDébito.Checked = transacción.EsDebito;
            //ckHabilitada.Checked = transacción.Estado == 1;
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
                short i;
                if (!Int16.TryParse(txtDuración.Text, out i))
                {
                    i = 0;
                }
                return i;
            }
        }

        public decimal Importe1Vto
        {
            get
            {
                decimal i;
                if (!Decimal.TryParse(txtImporte1Vto.Text, out i))
                {
                    i = 0;
                }
                return i;
            }
        }

        public decimal Importe2Vto
        {
            get
            {
                decimal i;
                if (!Decimal.TryParse(txtImporte2Vto.Text, out i))
                {
                    i = 0;
                }
                return i;
            }
        }

        public decimal Importe3Vto
        {
            get
            {
                decimal i;
                if (!Decimal.TryParse(txtImporte3Vto.Text, out i))
                {
                    i = 0;
                }
                return i;
            }
        }

        public short Estado
        {
            get
            {
                return (short)(ckEstado.Checked ? 1 : 0);
            }
        }
    }
}
