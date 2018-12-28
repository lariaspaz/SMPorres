using CustomLibrary.Lib.Extensions;
using SMPorres.Models;
using System;
using System.Windows.Forms;

namespace SMPorres.Forms.Usuarios
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
            txtNombre.Select();
        }

        public frmEdición(Usuario usuario)
            : this()
        {
            if (usuario == null)
            {
                this.Text = "Nuevo usuario";
                ckEstado.Checked = true;
            }
            else
            {
                this.Text = "Edición de usuario";
                txtNombre.Text = usuario.Nombre;
                txtNombreCompleto.Text = usuario.NombreCompleto;
                ckEstado.Checked = usuario.Estado == 1;
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
            bool result = true;
            result = ValidarTextBoxVacío(txtNombre, this, errorProvider1) &&
                        ValidarTextBoxVacío(txtNombreCompleto, this, errorProvider1);
            return result;
        }

        private bool ValidarTextBoxVacío(TextBox txt, IWin32Window window,
            ErrorProvider error)
        {
            bool result = true;
            if (String.IsNullOrWhiteSpace(txt.Text))
            {
                error.SetError(txt, "No puede estar vacío");
                new ToolTip().ShowError(window, txt, "No puede estar vacío");
                result = false;
            }
            else
            {
                error.SetError(txt, "");
            }
            return result;
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text.Trim();
            }
        }

        public string NombreCompleto
        {
            get
            {
                return txtNombreCompleto.Text.Trim();
            }
        }

        public byte Estado
        {
            get
            {
                return (byte)(ckEstado.Checked ? 1 : 0);
            }
        }
    }
}
