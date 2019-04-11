using Microsoft.Win32;
using SMPorres.Lib.Security;
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

namespace Utilidades
{
    public partial class frmConfigurarCadenaConexión : Form
    {
        public frmConfigurarCadenaConexión()
        {
            InitializeComponent();
        }

        private void btnEncriptarGrabar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtConnectionString.Text.Trim()))
            {
                MessageBox.Show("Ingrese la cadena de conexión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(txtClaveRegistroGrabar.Text.Trim()))
            {
                MessageBox.Show("Ingrese la clave del registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var connStr = StringCipher.Encrypt(txtConnectionString.Text, SMPorresEntities.ConnectionStringPassPhrase);
            string keyName = txtClaveRegistroGrabar.Text.Substring(0, txtClaveRegistroGrabar.Text.LastIndexOf("\\"));
            string value = txtClaveRegistroGrabar.Text.Substring(txtClaveRegistroGrabar.Text.LastIndexOf("\\") + 1);
            Registry.SetValue(keyName, value, connStr);

            var v = (string)Registry.GetValue(keyName, value, "");
            var d = StringCipher.Decrypt(v, SMPorresEntities.ConnectionStringPassPhrase);

            if (txtConnectionString.Text.Equals(d))
            {
                MessageBox.Show("La conexión se encriptó y grabó en el registro correctamente.", "Información", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La conexión no se pudo encriptar y/o grabar correctamente.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesencriptar_Click(object sender, EventArgs e)
        {
            string keyName = txtClaveRegistroLeer.Text.Substring(0, txtClaveRegistroLeer.Text.LastIndexOf("\\"));
            string value = txtClaveRegistroLeer.Text.Substring(txtClaveRegistroLeer.Text.LastIndexOf("\\") + 1);
            var v = (string)Registry.GetValue(keyName, value, "");
            txtUnencryptedConnectionString.Text = StringCipher.Decrypt(v, SMPorresEntities.ConnectionStringPassPhrase);
        }
    }
}
