using CustomLibrary.Extensions.Controls;
using SMPorres.Lib.AppForms;
using SMPorres.Models;
using SMPorres.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Pagos
{
    public partial class frmImportarPagosBSE : FormBase
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public string Archivo
        {
            get
            {
                return txtArchivo.Text.Trim();
            }
        }

        public frmImportarPagosBSE()
        {
            InitializeComponent();

            btnBuscar.Size = new Size(30, txtArchivo.ClientSize.Height + 2);
            //btnBuscar.Location = new Point(txtArchivo.ClientSize.Width - btnBuscar.Width, -1);
            btnBuscar.Dock = DockStyle.Right;
            btnBuscar.Cursor = Cursors.Default;
            txtArchivo.Controls.Add(btnBuscar);
            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(txtArchivo.Handle, 0xd3, (IntPtr)2, (IntPtr)(txtArchivo.Width << 16));
        }

        private void LeerArchivo()
        {
            //List<PagoBSE> archivo = new List<PagoBSE>();
            //string[] lines = File.ReadAllLines(_path);
            //foreach (string line in lines)
            //{
            //    //Corta por los tabs
            //    string[] campos = line.Split('\t');
            //    if (campos[0] == "succod") continue;
            //    PagoBSE tmp = new PagoBSE();
            //    tmp.CódigoSucursal = Int32.Parse(campos[0]);
            //    tmp.NombreSucursal = campos[1];
            //    tmp.Moneda = campos[2];
            //    tmp.Comprobante = Int32.Parse(campos[3]);
            //    tmp.TipoMovimiento = campos[4];
            //    tmp.Importe = decimal.Parse(campos[5]);
            //    tmp.FechaProceso = DateTime.Parse(campos[6]);
            //    tmp.CuilUsuario = campos[7];
            //    tmp.NombreUsuario = campos[8];
            //    tmp.Hora = Int32.Parse(campos[9]);
            //    tmp.CódigoBarra = campos[10];
            //    tmp.GrupoTerminal = campos[11];
            //    tmp.NroRendición = campos[12];
            //    tmp.FechaCobro = DateTime.Parse(campos[13]);
            //    dgvArchivoBSE.Rows.Add(tmp);              
            //}
        }

        private void registrarPago(PagoBSE tmp)
        {
            //Pago pago = new Pago();
            //pago.Id = tmp.Comprobante;
            //pago.Fecha = tmp.FechaCobro;
            //pago.FechaVto = LeerVto(tmp.CódigoBarra.Substring(11,5));
            //pago.IdMedioPago = 1;   //se debe leer de medios de pago
            //pago.ImportePagado = tmp.Importe/100; 
            ///*Éstos campos null podrían completarse al generar la boleta,*/
            //pago.PorcBeca = null;
            //pago.ImporteBeca = null;
            //pago.PorcDescPagoTermino = null;
            //pago.ImportePagoTermino = null;
            //pago.PorcRecargo = null;
            //pago.ImporteRecargo = null;
            //pago.Descripcion = null;

            //pago.IdArchivo = 1; //como generamos este ID?

            //if (PagosRepository.RegistrarPagoBSE(pago) == false)
            //{
            //    MessageBox.Show("No se pudo registrar pago del comprobante " + pago.Id, "Rendición BSE", MessageBoxButtons.OK);
            //}
            //else
            //{
            //    _archivosProcesados =+ _archivosProcesados;
            //}
        }

        private DateTime? LeerVto(string v)
        {
            int fJuliana = Convert.ToInt32(v);
            int day = fJuliana % 1000;
            int year = (fJuliana - day) / 1000;
            year = year += 2000;
            var vto = new DateTime(year, 1, 1);
            return vto.AddDays(day - 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = openFileDialog1.FileName;
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Archivo))
            {
                MessageBox.Show("El archivo no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CabecerasArchivosRepository.ExisteArchivo(TipoArchivo.RendiciónBSE, Archivo))
            {
                if (MessageBox.Show("El archivo ya ha sido cargado.\n¿Desea cargarlo nuevamente?",
                        "Precaución", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            var rendición = RendicionBSERepository.CargarRendición(Archivo);
            var query = from p in PagosRepository.ObtenerPagosRelacionados(rendición)
                        join r in rendición on p.Id equals Int32.Parse(r.Comprobante)
                        select new { p.Id, p.ImporteCuota, p.FechaVto, r.Comprobante, r.CodigoBarra };
            dgvDatos.SetDataSource(query);
        }
    }
}
