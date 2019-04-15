using SMPorres.Lib.AppForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void ThreadProc()
        {
            try
            {
                lblAcción.Text = "Conectando a la web";
                var repo = new Repositories.WebRepository();
                lblAcción.Text = "Obteniendo datos";
                var datos = repo.ObtenerDatos();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = datos.Count();
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                lblPorcentaje.Text = "0%";
                lblAcción.Text = "Procesando";
                ConsultasWeb.SMPSoapClient cliente = CrearCliente();
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
                            ShowError("Error al subir los datos de " + alumno.Nombre + " " + alumno.Apellido);
                            break;
                        }
                        progressBar1.PerformStep();
                        lblPorcentaje.Text = String.Format("{0}%", Math.Truncate((progressBar1.Value / (double)progressBar1.Maximum) * 100));
                    }
                    var conf = Repositories.ConfiguracionRepository.ObtenerConfiguracion();
                    cliente.ActualizarConfiguracion(conf.InteresPorMora);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cliente.Close();
                }
                MessageBox.Show("Los datos se subieron correctamente.\nFin del proceso.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ThreadAbortException)
            {
                //nada
            }
            catch (Exception ex)
            {
                ShowError("Error al subir los datos:\n", ex);
            }
            lblAcción.Text = "Fin del proceso";
            btnIniciarProceso.Image = Properties.Resources.control_play_blue;
            _stop = true;
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
