using CustomLibrary.Extensions.Controls;
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
    public partial class frmLeerArchivoBSE : Form
    {
        string _path = null;

        public frmLeerArchivoBSE()
        {
            InitializeComponent();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "ISMP<ddmmyyy>"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 
            dlg.ShowDialog();
            _path = dlg.FileName.Trim();
            lPath.Text = _path;
            

        }

        private void LeerArchivo()
        {
            List<ArchivoBSE> archivo = new List<ArchivoBSE>();

            string[] lines = File.ReadAllLines(_path);

            foreach (string line in lines)
            {
                //Corta por los tabs
                string[] campos = line.Split('\t');

                if (campos[0] == "succod") continue;

                ArchivoBSE tmp = new ArchivoBSE();
                tmp.CódigoSucursal = Int32.Parse(campos[0]);
                tmp.Sucursal = campos[1];
                tmp.Moneda = campos[2];
                tmp.Comprobante = Int32.Parse(campos[3]);
                tmp.TipoMov = campos[4];
                tmp.Importe = decimal.Parse(campos[5]);
                tmp.FechaProceso = DateTime.Parse(campos[6]);
                tmp.Cuil = campos[7];
                tmp.Usuario = campos[8];
                tmp.Hora = Int32.Parse(campos[9]);
                tmp.CódigoBarra = campos[10];
                tmp.GrupoTerminal = campos[11];
                tmp.NroRendicion = campos[12];
                tmp.FechaCobro = DateTime.Parse(campos[13]);
                dgvArchivoBSE.Rows.Add(tmp);
                //registrarPago(tmp);
            }
            cargarDGV(lines);
            int f = lines.Count() - 1;
            
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            List<ArchivoBSE> archivo = new List<ArchivoBSE>();

            string[] lines = File.ReadAllLines(_path);

            foreach (string line in lines)
            {
                //Corta por los tabs
                string[] campos = line.Split('\t');
                
                if (campos[0] == "succod") continue;

                ArchivoBSE tmp = new ArchivoBSE();
                tmp.CódigoSucursal = Int32.Parse(campos[0]);
                tmp.Sucursal = campos[1];
                tmp.Moneda = campos[2];
                tmp.Comprobante = Int32.Parse(campos[3]);
                tmp.TipoMov = campos[4];
                tmp.Importe = decimal.Parse(campos[5]);
                tmp.FechaProceso = DateTime.Parse(campos[6]);
                tmp.Cuil = campos[7];
                tmp.Usuario = campos[8];
                tmp.Hora = Int32.Parse(campos[9]);
                tmp.CódigoBarra = campos[10];
                tmp.GrupoTerminal = campos[11];
                tmp.NroRendicion = campos[12];
                tmp.FechaCobro = DateTime.Parse(campos[13]);

                registrarPago(tmp);
            }
            cargarDGV(lines);

        }

        private void cargarDGV(string[] lines)
        {


        }

        private void registrarPago(ArchivoBSE tmp)
        {

            Pago pago = new Pago();
            pago.Id = tmp.Comprobante;
            pago.Fecha = tmp.FechaCobro;

            pago.FechaVto = LeerVto(tmp.CódigoBarra.Substring(11,5));

            pago.IdMedioPago = 1;
            pago.ImportePagado = tmp.Importe; //separar en decimales

            pago.PorcBeca = null;
            pago.ImporteBeca = null;
            pago.PorcDescPagoTermino = null;
            pago.ImportePagoTermino = null;
            pago.PorcRecargo = null;
            pago.ImporteRecargo = null;
            pago.Descripcion = null;

            pago.IdArchivo = 1; //como generamos este ID?

            PagosRepository.RegistrarPagoBSE(pago);
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
    }
}
