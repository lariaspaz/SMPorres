using SMPorres.Lib.AppForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Web
{
    public partial class frmActualizarDatos : FormBase
    {
        private Thread _thread;
        private volatile bool _stop;

        public frmActualizarDatos()
        {
            InitializeComponent();
        }

        private void btnIniciarProceso_Click(object sender, EventArgs e)
        {
            if (_thread != null)
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
                var cliente = new ConsultasWeb.SMPSoapClient();
                var repo = new Repositories.WebRepository();
                var datos = repo.ObtenerDatos();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = datos.Count();
                progressBar1.Step = 1;
                progressBar1.Value = 0;
                label6.Text = "0%";
                foreach (var alumno in datos)
                {
                    if (_stop)
                    {
                        break;
                    }
                    cliente.ActualizarDatos(alumno);
                    progressBar1.PerformStep();
                    label6.Text = String.Format("{0}%", progressBar1.Value);
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
        }
    }
}
