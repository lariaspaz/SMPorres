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
        private IEnumerable<PagoBSE> _pagos;

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

            _pagos = from p in PagosBSERepository.ObtenerPagosRelacionados(rendición) orderby p.Id select p;
            foreach (var p in _pagos)
            {
                p.Válido = p.ImportePagado == p.ImporteAPagar && p.DetallePago != null;
            }

            //cómo es el código de barras de un pago de varias cuotas?
            var query = from p in _pagos
                        select new
                        {
                            p.Válido,
                            p.Id,
                            p.Comprobante,
                            p.Documento,
                            p.Alumno,
                            p.Carrera,
                            p.Curso,
                            p.FechaVto,
                            p.FechaPago,
                            ImporteAPagar = p.ImporteAPagar,
                            p.ImportePagado,
                            p.CodigoBarra
                        };
            dgvDatos.SetDataSource(query);
            toolStripStatusLabel1.Text = String.Format("Se han leído {0} filas", query.Count());
            btnGrabar.Enabled = query.Any(t => t.Válido);
        }

        private decimal? ObtenerImporteAPagar(int id, DateTime fechaVto)
        {
            var p = PagosRepository.ObtenerDetallePago(id, fechaVto);
            if (p == null) return null;
            System.Diagnostics.Debug.Print(String.Format("Id = {0} - FechaVto = {1} - Importe a pagar: {2}",
                id, fechaVto, p.ImportePagado));
            return p.ImportePagado;
        }

        private void dgvDatos_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var válido = (bool)dgvDatos.Rows[e.RowIndex].Cells[0].Value;
            if (!válido)
            {
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                dgvDatos.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvDatos.Columns[0].HeaderText = "Válido";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Código";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[2].HeaderText = "Comprobante";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[3].HeaderText = "Documento";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[4].HeaderText = "Alumno";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[5].HeaderText = "Carrera";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[6].HeaderText = "Curso";
            dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDatos.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvDatos.Columns[7].HeaderText = "Fecha Vto.";
            dgvDatos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[8].HeaderText = "Fecha Pago";
            dgvDatos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[9].HeaderText = "Importe a Pagar";
            dgvDatos.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[10].HeaderText = "Importe Pagado";
            dgvDatos.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDatos.Columns[11].HeaderText = "Código de Barras";
            dgvDatos.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            var pagos = _pagos.Where(p => p.Válido).ToList();
            string s = String.Format("Hay {0} pagos válidos de un total de {1}.\n¿Está seguro " + 
                        "que desea grabar esta rendición?", pagos.Count(), _pagos.Count());
            if (MessageBox.Show(s, "Grabar rendición", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    RendicionBSERepository.GrabarRendición(Archivo, pagos);
                    MessageBox.Show("Los datos se grabaron correctamente.", "Grabar rendición", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    _pagos = _pagos.Where(p => !p.Válido);
                    txtArchivo.Text = "";
                    dgvDatos.DataSource = null;
                    toolStripStatusLabel1.Text = "";
                    btnGrabar.Enabled = false;
                }
                catch (Exception ex)
                {
                    ShowError("Error al intentar grabar los pagos: \n", ex);
                }
            }
        }
    }
}
