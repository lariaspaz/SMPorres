using Microsoft.Win32;
using SMPorres.Lib.Security;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
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

            if (txtConnectionString.Text.Equals(d) && IntentarConectar())
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

        private bool IntentarConectar()
        {
            try
            {
                var model = "SMPorres";

                //string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\SMP\Cs";
                //string keyName = key.Substring(0, key.LastIndexOf("\\"));
                //string valueName = key.Substring(key.LastIndexOf("\\") + 1);
                //var value = (string)Microsoft.Win32.Registry.GetValue(keyName, valueName, "");
                //var conn = Lib.Security.StringCipher.Decrypt(value, ConnectionStringPassPhrase);

                var providerSB = new SqlConnectionStringBuilder(txtConnectionString.Text);

                var efConnection = new EntityConnectionStringBuilder();
                // or the config file based connection without provider connection string
                // var efConnection = new EntityConnectionStringBuilder(@"metadata=res://*/model1.csdl|res://*/model1.ssdl|res://*/model1.msl;provider=System.Data.SqlClient;");
                efConnection.Provider = "System.Data.SqlClient";
                efConnection.ProviderConnectionString = providerSB.ConnectionString;
                // based on whether you choose to supply the app.config connection string to the constructor
                efConnection.Metadata = string.Format("res://*/Models.{0}.csdl|res://*/Models.{0}.ssdl|res://*/Models.{0}.msl", model);
                //return efConnection.ToString();
                using (var db = new DbContext(efConnection.ToString()))
                {
                    db.Database.ExecuteSqlCommand("SELECT GETDATE()");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
