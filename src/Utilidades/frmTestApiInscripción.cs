using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilidades.Lib;

namespace Utilidades
{
    public partial class frmTestApiInscripción : Form
    {
        private HttpClient _client = null;

        public frmTestApiInscripción()
        {
            InitializeComponent();
            cbTipoDocumento.DataSource = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "DNI"),
                new KeyValuePair<int, string>(2, "CUIL"),
                new KeyValuePair<int, string>(3, "CUIT")
            };
            cbTipoDocumento.DisplayMember = "Value";
            cbTipoDocumento.ValueMember = "Key";

            cbSexo.DataSource = new List<KeyValuePair<char, string>>
            {
                new KeyValuePair<char, string>('M', "Masc."),
                new KeyValuePair<char, string>('F', "Fem.")
            };
            cbSexo.DisplayMember = "Value";
            cbSexo.ValueMember = "Key";

            cbCarrera.DataSource = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Técnico Superior en Instrumentación Quirúrgica"),
                new KeyValuePair<int, string>(2, "Técnico Superior en Radiología"),
                new KeyValuePair<int, string>(3, "Técnico Superior en Hemoterapia"),
                new KeyValuePair<int, string>(4, "Trabajador Social"),
                new KeyValuePair<int, string>(5, "Técnico Superior en Laboratorio de Análisis Clínicos")
            };
            cbCarrera.DisplayMember = "Value";
            cbCarrera.ValueMember = "Key";
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            InicializarCliente();
            AsyncHelpers.RunSync(() => Test());
        }

        private void InicializarCliente()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(txtEndpoint.Text);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.Timeout = Timeout.InfiniteTimeSpan;
            }
        }

        private async Task Test()
        {
            string qry = "api/test";
            HttpResponseMessage response = await _client.GetAsync(qry);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<List<string>>();
                MessageBox.Show("Test OK" + Environment.NewLine + "Resultado: " + String.Join(", ", result));
            }
            else
            {
                MessageBox.Show($"Error: {response.StatusCode} - {response.RequestMessage}");
            }
        }

        private bool ObtenerToken(ref string token)
        {
            InicializarCliente();
            token = "";
            string qry = "api/token?username=5f069cd8f8a54711bc09&password=8fVHrkjz4P8wruEf0tviB/aWnLDJpz7UpXFjLfpUVFE=";
            var byteArray = Encoding.ASCII.GetBytes("363dcec1-bf5f-40c2-af98-ea158cb07800.ismp.edu.ar:U2BGBPPbSgDVYUN+qSnG0/uyUkI+CzqhULDGlvCa4Iw=");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = AsyncHelpers.RunSync(() => _client.PostAsync(qry, null));
            if (response.IsSuccessStatusCode)
            {
                token = AsyncHelpers.RunSync(() => response.Content.ReadAsAsync<string>());
                return true;
            }
            else
            {
                MessageBox.Show($"Error: {response.StatusCode} - {response.RequestMessage}");
                return false;
            }
        }

        private void btnObtenerToken_Click(object sender, EventArgs e)
        {
            string token = "";
            if (ObtenerToken(ref token))
            {
                MessageBox.Show("Response OK" + Environment.NewLine + "Token: " + token);
            }
        }

        private class Alumno
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int IdTipoDocumento { get; set; }
            public decimal NroDocumento { get; set; }
            public System.DateTime FechaNacimiento { get; set; }
            public string EMail { get; set; }
            public string Direccion { get; set; }
            public char Sexo { get; set; }
            public int IdCarrera { get; set; }
        }

        private async void btnGrabarAlumno_Click(object sender, EventArgs e)
        {
            string token = "";
            if (ObtenerToken(ref token))
            {
                string qry = "api/inscripcion/alumno";
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var a = new Alumno
                {
                    IdTipoDocumento = 1,
                    FechaNacimiento = new DateTime(2000, DateTime.Now.Month, DateTime.Now.Day),
                    EMail = "test@gmail.com",
                    NroDocumento = Int64.Parse(txtDocumento.Text),
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Direccion = "",
                    Sexo = 'M',
                    IdCarrera = 1
                };
                var response = await _client.PostAsJsonAsync(qry, a);
                var msg = response.EnsureSuccessStatusCode();
                if (msg.IsSuccessStatusCode)
                {
                    MessageBox.Show("Grabó correctamente.");
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode} - {response.RequestMessage}");
                }
            }
        }
    }
}
