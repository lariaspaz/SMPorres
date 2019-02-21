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
using SMPorres.Reports.DataSet;

namespace SMPorres.Forms.Pagos
{
    public partial class frmInfCupónDePago : FormBase
    {
        private int _idPago;

        public frmInfCupónDePago(int idPago)
        {
            InitializeComponent();
            _idPago = idPago;
            AsignarDescripción();
        }

        private void AsignarDescripción()
        {
            var p = PagosRepository.ObtenerPago(_idPago);
            if (ckTodas.Checked)
            {
                txtDescripción.Text = String.Format("Todas las cuotas | {0} de {1}", p.PlanPago.Curso.Nombre,
                    p.PlanPago.Curso.Carrera.Nombre);
            }
            else
            {
                if (p.NroCuota > 0)
                {
                    txtDescripción.Text = String.Format("Cuota {0} de {1} | {2} de {3}", p.NroCuota, p.PlanPago.CantidadCuotas,
                        p.PlanPago.Curso.Nombre, p.PlanPago.Curso.Carrera.Nombre);
                }
                else
                {
                    txtDescripción.Text = String.Format("Matrícula | {0} de {1}", p.PlanPago.Curso.Nombre,
                        p.PlanPago.Curso.Carrera.Nombre);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            using (var dt = ObtenerDatos())
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    MostrarReporte(dt);
                }
                else
                {
                    ShowError("No hay datos para mostrar.");
                }
            }
        }

        private void MostrarReporte(DataTable dt)
        {
            using (var reporte = new CupónDePago())
            {
                reporte.Database.Tables["CupónPago"].SetDataSource(dt);
                using (var f = new frmReporte(txtDescripción.Text, reporte)) f.ShowDialog();
            }
        }

        private DataTable ObtenerDatos()
        {
            var cupón = new dsImpresiones.CupónPagoDataTable();
            var idPago = String.Format("{0:0000000}", _idPago);
            var fechaEmisión = String.Format("{0:dd/MM/yyyy}", Lib.Configuration.CurrentDate);
            var fechaVencimiento = String.Format("{0:dd/MM/yyyy}", dtFechaPago.Value);
            var pago = PagosRepository.ObtenerPago(_idPago);
            var nombre = String.Format("{0} {1}", pago.PlanPago.Alumno.Nombre, pago.PlanPago.Alumno.Apellido);
            var tipoDocumento = pago.PlanPago.Alumno.TipoDocumento.Descripcion;
            var documento = pago.PlanPago.Alumno.NroDocumento.ToString("N0");
            var curso = pago.PlanPago.Curso.Nombre;
            var carrera = pago.PlanPago.Curso.Carrera.Nombre;
            var fechaCompromiso = dtFechaPago.Value;

            if (ckTodas.Checked)
            {
                return GenerarDetalleTodas(cupón, idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento,
                    documento, curso, carrera, fechaCompromiso, pago);
            }
            else if (pago.NroCuota > 0)
            {
                return GenerarDetalleCuota(cupón, idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento,
                    documento, curso, carrera, fechaCompromiso, pago);
            }
            else
            {
                return GenerarMatrícula(cupón, idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento,
                    documento, curso, carrera, fechaCompromiso, pago);
            }
            //return cupón;
        }

        private DataTable GenerarMatrícula(dsImpresiones.CupónPagoDataTable cupón, string idPago, string fechaEmisión, string fechaVencimiento, string nombre, string tipoDocumento, string documento, string curso, string carrera, DateTime fechaCompromiso, Pago pago)
        {
            var importe = String.Format("{0:$ 0,0.00}", pago.ImporteCuota);
            var total = String.Format("{0:$ 0,0.00}", pago.ImporteCuota);
            string codBarra = GenerarCódigoBarras(idPago, pago.ImporteCuota);
            cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                carrera, curso, total, codBarra, "1", "Matrícula", importe);

            return cupón;
        }

        private DataTable GenerarDetalleCuota(dsImpresiones.CupónPagoDataTable cupón, string idPago,
            string fechaEmisión, string fechaVencimiento, string nombre, string tipoDocumento, string documento,
            string curso, string carrera, DateTime fechaCompromiso, Pago pago)
        {
            var impBase = pago.ImporteCuota;
            var importe = impBase.ToString("$ 0,0.00");
            string concepto = String.Format("Cuota Nº {0}", pago.NroCuota);
            cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                carrera, curso, "", "", "1", concepto, importe);

            var descBeca = (decimal)pago.PlanPago.PorcentajeBeca;
            decimal beca = 0;
            if (descBeca > 0)
            {
                beca = Math.Round(impBase * (descBeca / 100));
                importe = beca.ToString("$ -0,0.00");
                concepto = String.Format("Descuento por beca del %{0}", descBeca);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "2", concepto, importe);
            }

            var cuota = CuotasRepository.ObtenerCuotas().Where(c => c.NroCuota == pago.NroCuota).FirstOrDefault();
            if (cuota == null)
            {
                ShowError("Falta parametrizar la cuota " + pago.NroCuota);
                return null;
            }
            var vtoCuota = cuota.VtoCuota;
            var totalAPagar = (decimal)0;
            var impBecado = impBase - beca;
            if (fechaCompromiso <= vtoCuota)
            {
                var dpt = (decimal)(ConfiguracionRepository.ObtenerConfiguracion().DescuentoPagoTermino / 100);
                var descPagoTérmino = Math.Round(impBecado * dpt, 2);
                importe = descPagoTérmino.ToString("$ -0,0.00");
                concepto = "Descuento por pago a término";
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "3", concepto, importe);

                totalAPagar = impBase - beca - descPagoTérmino;
            }
            else
            {
                var porcRecargo = (ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
                var díasAtraso = Math.Truncate((fechaCompromiso - vtoCuota).TotalDays);
                var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                importe = recargoPorMora.ToString("$ 0,0.00");
                concepto = "Recargo por mora";
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "3", concepto, importe);

                totalAPagar = impBase - beca + recargoPorMora;
            }

            var codBarra = GenerarCódigoBarras(idPago, totalAPagar);
            foreach (Reports.DataSet.dsImpresiones.CupónPagoRow row in cupón.Rows)
            {
                row.Total = String.Format("{0:$ 0,0.00}", totalAPagar);
                row.CódigoBarra = codBarra;
            }

            return cupón;
        }

        private DataTable GenerarDetalleTodas(dsImpresiones.CupónPagoDataTable cupón, string idPago, string fechaEmisión, string fechaVencimiento, string nombre, string tipoDocumento, string documento, string curso, string carrera, DateTime fechaCompromiso, Pago pago)
        {
            var pagos = PagosRepository.ObtenerPagos(pago.IdPlanPago).Where(p => p.Fecha == null && p.NroCuota > 0);
            var min = pagos.Min(p => p.NroCuota);
            var max = pagos.Max(p => p.NroCuota);

            // CUOTAS *************************
            var totalBase = pagos.Sum(p => p.ImporteCuota);
            var importe = totalBase.ToString("$ 0,0.00");
            string concepto = String.Format("Cuotas de {0} a {1} ", min, max);
            cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                carrera, curso, "", "", "1", concepto, importe);

            // BECAS *************************
            decimal totalBecas = 0, totalPagoATérmino = 0, totalRecargos = 0;
            string cuotasBecadas = "", cuotasPagoATérmino = "", cuotasRecargoPorMora = "";
            decimal descBeca = 0;
            var cuotas = CuotasRepository.ObtenerCuotas();
            var totalAPagar = (decimal)0;
            foreach (var p in pagos)
            {
                var impBase = pago.ImporteCuota;
                decimal beca = 0;

                var p1 = PagosRepository.ObtenerPago(p.Id);
                if (p1.PlanPago.PorcentajeBeca > 0)
                {
                    descBeca = (decimal)p1.PlanPago.PorcentajeBeca;
                    beca = p.ImporteCuota * (descBeca / 100);
                    totalBecas += beca;
                    cuotasBecadas += String.IsNullOrEmpty(cuotasBecadas) ? p.NroCuota.ToString() : "," + p.NroCuota;
                }

                var cuota = cuotas.Where(c => c.NroCuota == p.NroCuota).FirstOrDefault();
                if (cuota == null)
                {
                    ShowError("Falta parametrizar la cuota " + p.NroCuota);
                    return null;
                }
                var vtoCuota = cuota.VtoCuota;
                var impBecado = impBase - beca;
                if (fechaCompromiso <= vtoCuota)
                {
                    var dpt = (decimal)(ConfiguracionRepository.ObtenerConfiguracion().DescuentoPagoTermino / 100);
                    var descPagoTérmino = Math.Round(impBecado * dpt, 2);
                    totalPagoATérmino += descPagoTérmino;
                    totalAPagar += impBase - beca - descPagoTérmino;
                    cuotasPagoATérmino += cuotasPagoATérmino == "" ? p.NroCuota.ToString() : "," + p.NroCuota;
                }
                else
                {
                    var porcRecargo = (ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
                    var díasAtraso = Math.Truncate((fechaCompromiso - vtoCuota).TotalDays);
                    var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                    var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                    totalRecargos += recargoPorMora;
                    totalAPagar += impBase - beca + recargoPorMora;
                    cuotasRecargoPorMora += cuotasRecargoPorMora == "" ? p.NroCuota.ToString() : "," + p.NroCuota;
                }
            }

            if (totalBecas > 0)
            {
                importe = totalBecas.ToString("$ -0,0.00");
                concepto = String.Format("Descuento de %{0} por becas de cuotas {1} ", descBeca, cuotasBecadas);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "2", concepto, importe);
            }

            if (totalPagoATérmino > 0)
            {
                importe = totalPagoATérmino.ToString("$ -0,0.00");
                concepto = String.Format("Descuento por pago a término de cuotas {0} ", cuotasPagoATérmino);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "2", concepto, importe);
            }

            if (totalRecargos > 0)
            {
                importe = totalRecargos.ToString("$ 0,0.00");
                concepto = String.Format("Recargo por mora de cuotas {0} ", cuotasRecargoPorMora);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "2", concepto, importe);
            }

            var codBarra = GenerarCódigoBarras(idPago, totalAPagar);
            foreach (dsImpresiones.CupónPagoRow row in cupón.Rows)
            {
                row.Total = String.Format("{0:$ 0,0.00}", totalAPagar);
                row.CódigoBarra = codBarra;
            }

            return cupón;
        }

        private string GenerarCódigoBarras(string idPago, decimal total)
        {
            const string códigoLink = "9015";
            //var fechaVtoJuliano = String.Format("{0:yy}{1:000}", dtFechaPago.Value, (dtFechaPago.Value - new DateTime(dtFechaPago.Value.Year, 1, 1)).TotalDays);
            var fechaVtoJuliano = String.Format("{0:yy}{1:000}", dtFechaPago.Value, dtFechaPago.Value.DayOfYear);
            //var importe = Math.Truncate(total).ToString("00000000") + ((total - Math.Truncate(total)) * 100).ToString("00");
            var importe = Math.Truncate(total * 100).ToString("0000000000");
            var códigoBarra = códigoLink + idPago + fechaVtoJuliano + importe;
            códigoBarra += Lib.Calculos.DígitoVerificador.CalculaDigitoVerificador(códigoBarra);
            return códigoBarra;
        }

        private void ckTodas_CheckedChanged(object sender, EventArgs e)
        {
            AsignarDescripción();
        }
    }
}
