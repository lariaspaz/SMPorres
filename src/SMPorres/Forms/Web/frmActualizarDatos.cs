using SMPorres.Lib.AppForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SMPorres.Forms.Web
{
    public partial class frmActualizarDatos : FormBase
    {
        private Thread _thread;
        private volatile bool _stop = true;

        public frmActualizarDatos()
        {
            InitializeComponent();
        }

        private void btnIniciarProceso_Click(object sender, EventArgs e)
        {
            if (!_stop)
            {
                btnIniciarProceso.Image = Properties.Resources.control_play_blue;
                _stop = true;
                _thread = null;
            }
            else
            {
                btnIniciarProceso.Image = Properties.Resources.control_stop_blue;
                _stop = false;
                _thread = new Thread(new ThreadStart(ThreadProc));
                _thread.Start();
            }
        }

        public string Acción
        {
            get
            {
                return lblAcción.Text;
            }
            set
            {
                //lblAcción.Text = value;
                lblAcción.Invoke((MethodInvoker)delegate { lblAcción.Text = value; });
            }
        }

        private void ThreadProc()
        {
            try
            {
                Acción = "Conectando a la web";
                var repo = new Repositories.WebRepository();
                Acción = "Obteniendo datos";
                var datos = repo.ObtenerDatos();
                InicializarProgreso(datos);
                Acción = "Procesando";
                ConsultasWeb.SMPSoapClient cliente = CrearCliente();
                bool error = false;
                try
                {
                    foreach (var alumno in datos)
                    {
                        if (_stop)
                        {
                            break;
                        }
                        if (!cliente.ActualizarDatos(alumno))
                        {
                            XmlSerializer xsSubmit = new XmlSerializer(typeof(ConsultasWeb.Alumno));
                            var xml = "";
                            using (var sww = new StringWriter())
                            {
                                using (XmlWriter writer = XmlWriter.Create(sww))
                                {
                                    xsSubmit.Serialize(writer, alumno);
                                    xml = sww.ToString(); // Your XML
                                    File.WriteAllText(Path.ChangeExtension(Application.ExecutablePath, ".upload.xml"), xml);
                                }
                            }
                            string s = String.Format("No se pudieron subir los datos del alumno: " +
                                "\nNº Documento:{0}\nNombre: {1}, {2}\nID: {3}", alumno.NroDocumento,
                                alumno.Apellido, alumno.Nombre, alumno.Id);
                            ShowError(s);
                            error = true;
                            break;
                        }
                        AvanzarProgreso();
                    }
                    var conf = Repositories.ConfiguracionRepository.ObtenerConfiguracion();
                    cliente.ActualizarConfiguracion(conf.InteresPorMora);
                }
                //catch (Exception)
                //{
                //    throw;
                //}
                finally
                {
                    cliente.Close();
                }
                if (!error)
                {
                    MessageBox.Show("Los datos se subieron correctamente.\nFin del proceso.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ThreadAbortException)
            {
                //nada
            }
            catch (Exception ex)
            {
                ShowError("Error al subir los datos:\n", ex);
            }
            finally
            {
                Acción = "Fin del proceso";
                FinalizarProgreso();
                _stop = true;
            }
        }

        private void FinalizarProgreso()
        {
            btnIniciarProceso.Invoke((MethodInvoker)delegate
            {
                btnIniciarProceso.Image = Properties.Resources.control_play_blue;
            });
        }

        private void AvanzarProgreso()
        {
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.PerformStep();
            });
            lblPorcentaje.Invoke((MethodInvoker)delegate
            {
                lblPorcentaje.Text = String.Format("{0}%", Math.Truncate((progressBar1.Value / (double)progressBar1.Maximum) * 100));
            });
        }

        private void InicializarProgreso(IEnumerable<ConsultasWeb.Alumno> datos)
        {
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = datos.Count();
                progressBar1.Step = 1;
                progressBar1.Value = 0;
            });
            lblPorcentaje.Invoke((MethodInvoker)delegate
            {
                lblPorcentaje.Text = "0%";
            });
        }

        private static ConsultasWeb.SMPSoapClient CrearCliente()
        {
            //Specify the binding to be used for the client.
            BasicHttpBinding binding = new BasicHttpBinding();

            //Specify the address to be used for the client.
            EndpointAddress address =
               new EndpointAddress(Repositories.ConfiguracionRepository.ObtenerConfiguracion().EndpointAddress);

            return new ConsultasWeb.SMPSoapClient(binding, address);
        }
    }
}
