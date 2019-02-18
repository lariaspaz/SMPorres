﻿using SMPorres.Lib.AppForms;
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
                txtDescripción.Text = String.Format("Cuota {0} de {1} | {2} de {3}", p.NroCuota, p.PlanPago.CantidadCuotas,
                    p.PlanPago.Curso.Nombre, p.PlanPago.Curso.Carrera.Nombre);
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
            var idPago = String.Format("{0:0000000}", _idPago);
            var fechaEmisión = String.Format("{0:dd/MM/yyyy}", Lib.Configuration.CurrentDate);
            var fechaVencimiento = dtFechaPago.Text;
            var pago = PagosRepository.ObtenerPago(_idPago);
            var nombre = String.Format("{0} {1}", pago.PlanPago.Alumno.Nombre, pago.PlanPago.Alumno.Apellido);
            var tipoDocumento = pago.PlanPago.Alumno.TipoDocumento.Descripcion;
            var documento = pago.PlanPago.Alumno.NroDocumento.ToString("N0");
            var curso = pago.PlanPago.Curso.Nombre;
            var carrera = pago.PlanPago.Curso.Carrera.Nombre;
            var fechaCompromiso = dtFechaPago.Value;

            if (ckTodas.Checked)
            {
                var pagos = pago.PlanPago.Pagos.Where(p => p.Fecha == null && p.NroCuota > 0);
                var min = pagos.Min(p => p.NroCuota);
                var max = pagos.Max(p => p.NroCuota);

                var impBase = pagos.Sum(p => p.ImporteCuota);
                var importe = impBase.ToString("$ 0,0.00");
                string concepto = String.Format("Cuotas de {0} a {1} ", min, max);
                cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                    carrera, curso, "", "", "1", concepto, importe);

                var pb = pagos
                            .Where(p => p.PlanPago.PorcentajeBeca > 0)
                            .Select(p => p.ImporteCuota * ((decimal) p.PlanPago.PorcentajeBeca) / 100);
                decimal beca = pb.Sum();
                if (beca > 0)
                {
                    string s = String.Join(", ", pagos
                                                    .Where(p => p.PlanPago.PorcentajeBeca > 0)
                                                    .Select(p => p.NroCuota.ToString()));
                    importe = beca.ToString("$ -0,0.00");
                    concepto = String.Format("Descuentos por becas de cuotas %{0}", s);
                    cupón.AddCupónPagoRow(idPago, fechaEmisión, fechaVencimiento, nombre, tipoDocumento, documento,
                        carrera, curso, "", "", "2", concepto, importe);
                }

                //CuotasRepository.ObtenerCuotas().Any(
                //var cuota = CuotasRepository.ObtenerCuotas().Where(c => c.NroCuota == pago.NroCuota).FirstOrDefault();
                //if (cuota == null)
                //{
                //    ShowError("Falta parametrizar la cuota " + pago.NroCuota);
                //    return null;
                //}

            }
            if (pago.NroCuota > 0)
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
            }
            else
            {
                var importe = String.Format("{0:$ 0,0.00}", pago.ImporteCuota);
                var total = String.Format("{0:$ 0,0.00}", pago.ImporteCuota);
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

        private void ckTodas_CheckedChanged(object sender, EventArgs e)
        {
            AsignarDescripción();
        }
    }
}
