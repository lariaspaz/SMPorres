using SMPorres.Lib.AppForms;
using SMPorres.Models;
using SMPorres.Reports.Designs;
using SMPorres.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Pagos
{
    public partial class frmInfCupónDePago : FormBase
    {
        private int _idPago;

        private enum TipoImpresión
        {
            Matrícula,
            Cuota
        }

        public frmInfCupónDePago(int idPago)
        {
            InitializeComponent();

            _idPago = idPago;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            using (var dt = ObtenerDatos())
            {
                if (dt.Rows.Count > 0)
                {
                    MostrarReporte(dt);
                }
                else
                {
                    ShowError("No hay datos para mostrar.");
                }
            }
        }

        private static void MostrarReporte(DataTable dt)
        {
            using (var reporte = new CupónDePago())
            {
                reporte.Database.Tables["CupónPago"].SetDataSource(dt);
                using (var f = new frmReporte("", reporte)) f.ShowDialog();
            }
        }

        private DataTable ObtenerDatos()
        {
            var cupón = new Reports.DataSet.dsImpresiones.CupónPagoDataTable();
            //var row = cupón.NewCupónPagoRow();
            var idPago = String.Format("{0:0000000}", _idPago);
            var fechaEmisión = String.Format("{0:dd/MM/yyyy}", Lib.Configuration.CurrentDate);
            var fechaVencimiento = dtFechaPago.Text;
            var pago = PagosRepository.ObtenerPago(_idPago);
            var nombre = String.Format("{0} {1}", pago.PlanPago.Alumno.Nombre, pago.PlanPago.Alumno.Apellido);
            var tipoDocumento = pago.PlanPago.Alumno.TipoDocumento.Descripcion;
            var documento = pago.PlanPago.Alumno.NroDocumento.ToString("N0");
            var curso = pago.PlanPago.Curso.Nombre;
            var carrera = pago.PlanPago.Curso.Carrera.Nombre;
            if (pago.NroCuota > 0)
            {
                var impBase = pago.ImporteCuota;
                var importe = impBase.ToString("C2");
                string concepto = String.Format("Cuota Nº {0}", pago.NroCuota);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "1", concepto, importe);

                var fechaCompromiso = dtFechaPago.Value;
                var descBeca = (decimal)pago.PlanPago.PorcentajeBeca / 100;
                decimal impBecado = impBase;
                if (descBeca > 0)
                {
                    impBecado = impBase * descBeca;
                    importe = impBecado.ToString("C2");
                    concepto = String.Format("Descuento por beca del %{0}", Math.Truncate(descBeca * 100));
                    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                        carrera, curso, "", "", "1", concepto, importe);
                }

                var cuota = CuotasRepository.ObtenerCuotas().Where(c => c.NroCuota == pago.NroCuota).First();
                var vtoCuota = cuota.VtoCuota;
                var totalAPagar = (decimal)0;
                if (fechaCompromiso <= vtoCuota)
                {
                    var descPagoTérmino = impBecado * (decimal)(ConfiguracionRepository.ObtenerConfiguracion().DescuentoPagoTermino / 100);
                    importe = descPagoTérmino.ToString("C2");
                    concepto = "Descuento por pago a término";
                    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                        carrera, curso, "", "", "1", concepto, importe);

                    totalAPagar = impBecado - descPagoTérmino;
                }
                else
                {
                    var porcRecargo = ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora;
                    var díasAtraso = (Lib.Configuration.CurrentDate - dtFechaPago.Value).TotalDays;
                    var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                    var recargoPorMora = pago.ImporteCuota * porcRecargoTotal;
                    importe = recargoPorMora.ToString("C2");
                    concepto = "Recargo por mora";
                    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                        carrera, curso, "", "", "1", concepto, importe);

                    totalAPagar = impBecado + recargoPorMora;
                }

                var codBarra = GenerarCódigoBarras(idPago, totalAPagar);
                foreach (Reports.DataSet.dsImpresiones.CupónPagoRow row in cupón.Rows)
                {
                    row.Total = String.Format("{0:C2}", totalAPagar);
                    row.CódigoBarra = codBarra;
                }
            }
            else
            {
                var importe = String.Format("{0:C2}", pago.ImporteCuota);
                var total = String.Format("{0:C2}", pago.ImporteCuota);
                string codBarra = GenerarCódigoBarras(idPago, pago.ImporteCuota);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, total, codBarra, "1", "Matrícula", importe);
            }
            return cupón;
        }

        private string GenerarCódigoBarras(string idPago, decimal total)
        {
            var fechaVtoJuliano = String.Format("{0:yy}{1:000}", dtFechaPago.Value, (dtFechaPago.Value - new DateTime(dtFechaPago.Value.Year, 1, 1)).TotalDays);
            var importe = Math.Truncate(total).ToString("00000000") + ((total - Math.Truncate(total)) * 100).ToString("00");
            var códigoBarra = idPago + fechaVtoJuliano + importe;
            códigoBarra += Lib.Calculos.DígitoVerificador.CalculaDigitoVerificador(códigoBarra, "10");
            return códigoBarra;
        }

        private DataTable CrearDataTable()
        {
            var dt = new Reports.DataSet.dsImpresiones.CupónPagoDataTable();
            var rowCupónPagoRow = (Reports.DataSet.dsImpresiones.CupónPagoRow)(dt.NewRow());
            dt.Rows.Add(rowCupónPagoRow);
            return dt;
        }
    }
}
