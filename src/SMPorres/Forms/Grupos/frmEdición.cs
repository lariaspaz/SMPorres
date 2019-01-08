using SMPorres.Lib.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPorres.Models;

namespace SMPorres.Forms.GrupoUsuarios
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
            this.Text = "Nueva transacción";
            txtGrupo.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(Grupos grupo) : this()
        {
            this.Text = "Edición de transacción";
            txtGrupo.Text = grupo.Descripcion;
            ckEstado.Checked = grupo.Estado == 1;
        }

        public string Grupo
        {
            get
            {
                return txtGrupo.Text;
            }
        }

        public byte Estado
        {
            get
            {
                return (byte) (ckEstado.Checked ? 1 : 0);
            }
        }

        private bool ValidarDatos()
        {
            return
                _validator.Validar(txtGrupo, Grupo !="", "Debe contener texto");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmEdición_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnCancelar.PerformClick();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
