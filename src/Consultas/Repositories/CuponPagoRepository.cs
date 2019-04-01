using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class CuponPagoRepository
    {
        public DataTable ObtenerDatos(bool todas, int idPago, DateTime fechaCompromiso)
        {
            var cupón = new dsConsultas.CupónPagoDataTable();
            var idPagoStr = String.Format("{0:0000000}", 0);
            var fechaEmisión = String.Format("{0:dd/MM/yyyy}", Lib.Configuration.CurrentDate);
            var fechaVencimiento = String.Format("{0:dd/MM/yyyy}", fechaCompromiso);
            var pago = new PagosRepository().ObtenerPago(idPago);
            var nombre = String.Format("{0} {1}", pago.CursoAlumnoWeb.AlumnoWeb.Nombre, pago.CursoAlumnoWeb.AlumnoWeb.Apellido);
            var tipoDocumento = pago.CursoAlumnoWeb.AlumnoWeb.TipoDocumento;
            var documento = pago.CursoAlumnoWeb.AlumnoWeb.NroDocumento.ToString("N0");
            var curso = pago.CursoAlumnoWeb.Curso;
            var carrera = pago.CursoAlumnoWeb.Carrera;

            if (todas)
            {
                return GenerarDetalleTodas(cupón, idPagoStr, fechaEmisión, fechaVencimiento, nombre, tipoDocumento,
                    documento, curso, carrera, fechaCompromiso, pago);
            }
            else if (pago.NroCuota > 0)
            {
                return GenerarDetalleCuota(cupón, idPagoStr, fechaEmisión, fechaVencimiento, nombre, tipoDocumento,
                    documento, curso, carrera, fechaCompromiso, pago);
            }
            else
            {
                return GenerarMatrícula(cupón, idPagoStr, fechaEmisión, fechaVencimiento, nombre, tipoDocumento,
                    documento, curso, carrera, fechaCompromiso, pago);
            }
        }

        private DataTable GenerarDetalleTodas(dsConsultas.CupónPagoDataTable cupón, string idPago, string fechaEmisión,
            string fechaVencimiento, string nombre, string tipoDocumento, string documento, string curso, string carrera,
            DateTime fechaCompromiso, PagoWeb pago)
        {
            return null;

            //var pagos = PagosRepository.ObtenerPagos(pago.IdPlanPago).Where(p => p.Fecha == null && p.NroCuota > 0);
            //var min = pagos.Min(p => p.NroCuota);
            //var max = pagos.Max(p => p.NroCuota);

            //// CUOTAS *************************
            //var totalBase = pagos.Sum(p => p.ImporteCuota);
            //var importe = totalBase.ToString("$ 0,0.00");
            //string concepto = String.Format("Cuotas de {0} a {1} ", min, max);
            //cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
            //    carrera, curso, "", "", "1", concepto, importe);

            //// BECAS *************************
            //decimal totalBecas = 0, totalPagoATérmino = 0, totalRecargos = 0;
            //string cuotasBecadas = "", cuotasPagoATérmino = "", cuotasRecargoPorMora = "";
            //decimal descBeca = 0;
            //var cuotas = CuotasRepository.ObtenerCuotas();
            //var totalAPagar = (decimal)0;
            //foreach (var p in pagos)
            //{
            //    var impBase = pago.ImporteCuota;
            //    decimal beca = 0;

            //    var p1 = PagosRepository.ObtenerPago(p.Id);
            //    if (p1.PlanPago.PorcentajeBeca > 0)
            //    {
            //        descBeca = (decimal)p1.PlanPago.PorcentajeBeca;
            //        beca = p.ImporteCuota * (descBeca / 100);
            //        totalBecas += beca;
            //        cuotasBecadas += String.IsNullOrEmpty(cuotasBecadas) ? p.NroCuota.ToString() : "," + p.NroCuota;
            //    }

            //    var cuota = cuotas.Where(c => c.NroCuota == p.NroCuota).FirstOrDefault();
            //    if (cuota == null)
            //    {
            //        ShowError("Falta parametrizar la cuota " + p.NroCuota);
            //        return null;
            //    }
            //    var vtoCuota = cuota.VtoCuota;
            //    var impBecado = impBase - beca;
            //    if (fechaCompromiso <= vtoCuota)
            //    {
            //        var dpt = (decimal)(ConfiguracionRepository.ObtenerConfiguracion().DescuentoPagoTermino / 100);
            //        var descPagoTérmino = Math.Round(impBecado * dpt, 2);
            //        totalPagoATérmino += descPagoTérmino;
            //        totalAPagar += impBase - beca - descPagoTérmino;
            //        cuotasPagoATérmino += cuotasPagoATérmino == "" ? p.NroCuota.ToString() : "," + p.NroCuota;
            //    }
            //    else
            //    {
            //        var porcRecargo = (ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
            //        var díasAtraso = Math.Truncate((fechaCompromiso - vtoCuota).TotalDays);
            //        var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
            //        var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
            //        totalRecargos += recargoPorMora;
            //        totalAPagar += impBase - beca + recargoPorMora;
            //        cuotasRecargoPorMora += cuotasRecargoPorMora == "" ? p.NroCuota.ToString() : "," + p.NroCuota;
            //    }
            //}

            //if (totalBecas > 0)
            //{
            //    importe = totalBecas.ToString("$ -0,0.00");
            //    concepto = String.Format("Descuento de %{0} por becas de cuotas {1} ", descBeca, cuotasBecadas);
            //    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
            //        carrera, curso, "", "", "2", concepto, importe);
            //}

            //if (totalPagoATérmino > 0)
            //{
            //    importe = totalPagoATérmino.ToString("$ -0,0.00");
            //    concepto = String.Format("Descuento por pago a término de cuotas {0} ", cuotasPagoATérmino);
            //    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
            //        carrera, curso, "", "", "2", concepto, importe);
            //}

            //if (totalRecargos > 0)
            //{
            //    importe = totalRecargos.ToString("$ 0,0.00");
            //    concepto = String.Format("Recargo por mora de cuotas {0} ", cuotasRecargoPorMora);
            //    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
            //        carrera, curso, "", "", "2", concepto, importe);
            //}

            //var codBarra = GenerarCódigoBarras(idPago, totalAPagar);
            //foreach (dsImpresiones.CupónPagoRow row in cupón.Rows)
            //{
            //    row.Total = String.Format("{0:$ 0,0.00}", totalAPagar);
            //    row.CódigoBarra = codBarra;
            //}

            //return cupón;
        }

        private DataTable GenerarMatrícula(dsConsultas.CupónPagoDataTable cupón, string idPago, string fechaEmisión,
            string fechaVencimiento, string nombre, string tipoDocumento, string documento, string curso, string carrera,
            DateTime fechaCompromiso, PagoWeb pago)
        {
            var importe = String.Format("{0:$ 0,0.00}", pago.ImporteCuota);
            var total = String.Format("{0:$ 0,0.00}", pago.ImporteCuota);
            string codBarra = GenerarCódigoBarras(idPago, pago.ImporteCuota, fechaCompromiso);
            cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                carrera, curso, total, codBarra, "1", "Matrícula", importe);

            return cupón;
        }

        private DataTable GenerarDetalleCuota(dsConsultas.CupónPagoDataTable cupón, string idPago, string fechaEmisión,
            string fechaVencimiento, string nombre, string tipoDocumento, string documento, string curso, string carrera,
            DateTime fechaCompromiso, PagoWeb p)
        {
            var impBase = p.ImporteCuota;
            var importe = impBase.ToString("$ 0,0.00");
            string concepto = String.Format("Cuota Nº {0}", p.NroCuota);
            cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                carrera, curso, "", "", "1", concepto, importe);

            if (p.ImporteBeca > 0)
            {
                importe = p.ImporteBeca.Value.ToString("$ -0,0.00");
                concepto = String.Format("Descuento por beca del %{0}", p.PorcentajeBeca);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "2", concepto, importe);
            }

            if (fechaCompromiso <= p.FechaVto)
            {
                importe = p.ImportePagoTermino.Value.ToString("$ -0,0.00");
                concepto = "Descuento por pago a término";
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "3", concepto, importe);
            }
            else
            {
                importe = p.ImporteRecargo.Value.ToString("$ 0,0.00");
                concepto = String.Format("Recargo por mora - Vencida el {0:dd/MM/yyyy}", p.FechaVto);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "3", concepto, importe);
            }

            var codBarra = GenerarCódigoBarras(idPago, p.ImportePagado.Value, fechaCompromiso);
            foreach (dsConsultas.CupónPagoRow row in cupón.Rows)
            {
                row.Total = String.Format("{0:$ 0,0.00}", p.ImportePagado.Value);
                row.CódigoBarra = codBarra;
            }

            return cupón;
        }

        private string GenerarCódigoBarras(string idPago, decimal total, DateTime fechaCompromiso)
        {
            const string códigoLink = "9016";
            var fechaVtoJuliano = String.Format("{0:yy}{1:000}", fechaCompromiso, fechaCompromiso.DayOfYear);
            var importe = Math.Truncate(total * 100).ToString("0000000000");
            var códigoBarra = códigoLink + idPago + fechaVtoJuliano + importe;
            códigoBarra += Lib.Calculos.DígitoVerificador.CalculaDigitoVerificador(códigoBarra);
            códigoBarra = "*" + códigoBarra + "*";
            return códigoBarra;
        }
    }
}