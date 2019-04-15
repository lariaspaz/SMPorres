using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilidades
{
    public partial class frmConsultasWeb : Form
    {
        public frmConsultasWeb()
        {
            InitializeComponent();
            var c = new ConsultasWeb.SMPSoapClient();
            //txtEndpoint.Text = c.Endpoint.Address.Uri.AbsolutePath;
            txtEndpoint.Text = c.Endpoint.Address.Uri.OriginalString;
        }

        private static ConsultasWeb.SMPSoapClient CrearCliente(string endpointAddress)
        {
            //Specify the binding to be used for the client.
            BasicHttpBinding binding = new BasicHttpBinding();

            //Specify the address to be used for the client.
            EndpointAddress address = new EndpointAddress(endpointAddress);

            return new ConsultasWeb.SMPSoapClient(binding, address);
        }

        private void btnVerificarConexion_Click(object sender, EventArgs e)
        {
            var cliente = CrearCliente(txtEndpoint.Text);
            cliente.TestAlive();
            cliente.Close();
            MessageBox.Show("Todo ok.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLimpiarBD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea eliminar todos los datos en la web de consultas?", 
                "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            try
            {
                var cliente = CrearCliente(txtEndpoint.Text);
                cliente.ActualizarDatos(new SMPorres.ConsultasWeb.Alumno {
                    Id = 0,
                    Nombre = "38F73513-C569-4C08-B9DD-BDA2A0367605"
                });
                cliente.Close();
                MessageBox.Show("Los datos se eliminaron correctamente.", "Información", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
