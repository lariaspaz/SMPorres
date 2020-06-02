using Consultas.Lib;
using Consultas.Lib.Security;
using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Consultas.Models.WebServices;
using System.Collections;

namespace Consultas.Repositories
{
    public class PagosRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Actualizar(SMPorresEntities db, int idCursoAlumno, Models.WebServices.Pago pago)
        {
            var p = db.PagosWeb.Find(pago.Id);
            bool insertar = p == null;
            if (insertar)
            {
                p = new PagoWeb();
                p.Id = pago.Id;
                p.IdCursoAlumno = idCursoAlumno;
            }
            p.IdPlanPago = pago.IdPlanPago;
            p.NroCuota = (short)pago.NroCuota;
            p.ImporteCuota = pago.ImporteCuota;
            p.ImporteBeca = pago.ImporteBeca;
            p.ImporteRecargo = pago.ImporteRecargo;
            p.ImportePagado = pago.ImportePagado;
            p.Fecha = (pago.Fecha == default(DateTime)) ? null : pago.Fecha;
            p.FechaVto = pago.FechaVto;
            p.ImportePagoTermino = pago.ImportePagoTérmino;
            p.PorcentajeBeca = pago.PorcentajeBeca;
            p.FechaVtoPagoTermino = pago.FechaVtoPagoTérmino;
            p.TipoBeca = pago.TipoBeca;
            p.Estado = pago.Estado;
            if (insertar)
            {
                db.PagosWeb.Add(p);
            }
            db.SaveChanges();
        }

        public IEnumerable<PagoWeb> ObtenerPagos(int idCursoAlumno)
        {
            using (var db = new SMPorresEntities())
            {
                var qry = from p in db.PagosWeb
                          where p.IdCursoAlumno == idCursoAlumno &&
                                  p.CursoAlumnoWeb.AlumnoWeb.Id == Session.CurrentUserId &&
                                  p.Estado != (byte)EstadoPago.Baja
                          orderby p.NroCuota, p.Id
                          select p;
                return qry.ToList();
            }
        }

        internal void EliminarPagos(SMPorresEntities db, List<CursoAlumno> cursosAlumnos)
        {
            foreach (var item in cursosAlumnos)
            {
                var pagos = db.PagosWeb.Where(p => p.IdCursoAlumno == item.Id);
                foreach (var i in pagos)
                {
                    var p = db.PagosWeb.Find(i.Id);
                    db.PagosWeb.Remove(p);
                    db.SaveChanges();
                }
            }
        }

        public int ObtenerIdCuota(int nroCuota)
        {
            using (var db = new SMPorresEntities())
            {
                PagoWeb p2 = new PagoWeb();

                if (nroCuota > 0)
                {
                    p2 = (from p in db.PagosWeb
                          where p.CursoAlumnoWeb.AlumnoWeb.Id == Session.CurrentUserId &&
                                p.NroCuota == nroCuota &&
                                p.Estado != (byte)EstadoPago.Baja
                          select p).FirstOrDefault();
                }
                else
                {
                    p2 = (from p in db.PagosWeb
                          where p.CursoAlumnoWeb.AlumnoWeb.Id == Session.CurrentUserId &&
                                p.NroCuota == nroCuota &&
                                p.Estado != (byte)EstadoPago.Baja
                          select p)
                          .OrderBy(z => z.Id)
                          .FirstOrDefault();
                }
                return (p2 == null) ? 0 : p2.Id;
            }
        }

        public PagoWeb ObtenerPago(int idPago)
        {
            using (var db = new SMPorresEntities())
            {
                var p = db.PagosWeb.Find(idPago);
                if (p != null)
                {
                    db.Entry(p).Reference(p1 => p1.CursoAlumnoWeb).Load();
                    db.Entry(p.CursoAlumnoWeb).Reference(ca => ca.AlumnoWeb).Load();
                }
                return p;
            }
        }

        public static PagoWeb ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
        {
            var pago = new PagosRepository().ObtenerPago(idPago);

            pago.ImporteRecargo = 0;
            pago.ImportePagado = pago.ImporteCuota;

            var totalAPagar = (decimal)0;
            //var impBecado = pago.ImporteCuota - pago.ImporteBeca ?? 0;
            if (fechaCompromiso <= pago.FechaVto)
            {
                //Los becados no tienen descuento por pago a término
                _log.Debug($"fechaCompromiso = {fechaCompromiso}|pago.FechaVtoPagoTermino = {pago.FechaVtoPagoTermino}||pago.ImporteBeca = {pago.ImporteBeca}");
                if (fechaCompromiso > pago.FechaVtoPagoTermino || pago.ImporteBeca > 0)
                {
                    pago.ImportePagoTermino = 0; //la beca y el pago a término son excluyentes
                    totalAPagar = pago.ImporteCuota - pago.ImporteBeca ?? 0;
                }
                else
                {
                    _log.Debug($"fechaCompromiso > pago.FechaVtoPagoTermino - {fechaCompromiso} > {pago.FechaVtoPagoTermino}");
                    pago.ImporteBeca = 0;
                    totalAPagar = pago.ImporteCuota - pago.ImportePagoTermino ?? 0;
                }
            }
            else
            {
                _log.Debug($"fechaCompromiso <= pago.FechaVto - {fechaCompromiso} > {pago.FechaVto}");

                var impBecado = pago.ImporteCuota - pago.ImporteBeca ?? 0;
                decimal recargoPorMora = 0;

                #region Modelo de cálculo de recargo anterior
                //var porcRecargo = (new ConfiguracionRepository().ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
                //var díasAtraso = Math.Truncate((fechaCompromiso - pago.FechaVto).TotalDays);
                //var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                //if (pago.TipoBeca == (byte)TipoBeca.AplicaSiempre)
                //{
                //    recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                //}
                //else
                //{
                //    recargoPorMora = Math.Round(pago.ImporteCuota * porcRecargoTotal, 2);
                //    pago.ImporteBeca = 0;
                //}
                //totalAPagar = pago.ImporteCuota - (pago.ImporteBeca ?? 0) + recargoPorMora;
                //pago.ImporteRecargo = recargoPorMora;
                #endregion

                decimal beca = 0;
                recargoPorMora = CalcularMoraPorTramo(fechaCompromiso, pago, pago.ImporteCuota, impBecado);
                totalAPagar = pago.ImporteCuota - beca + recargoPorMora;
                if (recargoPorMora > 0 && (pago.TipoBeca != (byte)TipoBeca.AplicaSiempre))
                {
                    pago.ImporteBeca = beca;
                }
                pago.ImporteRecargo = recargoPorMora;
                _log.Debug($"totalAPagar = {totalAPagar}|pago.ImporteBeca = {pago.ImporteBeca}|pago.ImporteRecargo = {pago.ImporteRecargo}");
            }

            pago.ImportePagado = totalAPagar;
            return pago;
        }

        private static decimal CalcularMoraPorTramo(DateTime fechaCompromiso, PagoWeb pago, decimal impBase,
           decimal impBecado)
        {
            decimal recargoPorMora;
            var tasas = from t in TasasMoraRepository.ObtenerTasas()
                        select new TasaMora
                        {
                            Tasa = (t.Tasa / 100) / 30,
                            Desde = t.Desde < pago.FechaVto ? pago.FechaVto : t.Desde,
                            Hasta = t.Hasta > fechaCompromiso ? fechaCompromiso : t.Hasta.AddDays(1)
                        };
            tasas = tasas.Where(t => t.Desde <= t.Hasta);
            _log.Debug(String.Join("\n", tasas.Select(t => new { TasaDiaria = t.Tasa, t.Desde, t.Hasta })));
            _log.Debug("Tipo de beca: " + (TipoBeca)pago.TipoBeca);

            if (pago.TipoBeca == (byte)TipoBeca.AplicaSiempre)
            {
                _log.Debug("Importe becado: " + impBecado);
                _log.Debug(String.Join("\n", tasas.Select(t => new
                {
                    TotalDays = (t.Hasta - t.Desde).TotalDays,
                    Tasa = t.Tasa,
                    DaysXTasa = (t.Hasta - t.Desde).TotalDays * t.Tasa,
                    Total = impBecado * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa)
                })));

                recargoPorMora = tasas.Sum(
                            t => Math.Round(impBecado * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa), 2)
                        );
            }
            else
            {
                _log.Debug("Importe base: " + impBase);
                _log.Debug(String.Join("\n", tasas.Select(t => new
                {
                    TotalDays = (t.Hasta - t.Desde).TotalDays,
                    Tasa = t.Tasa,
                    DaysXTasa = (t.Hasta - t.Desde).TotalDays * t.Tasa,
                    Total = impBase * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa)
                })));

                recargoPorMora = tasas.Sum(
                            t => Math.Round(impBase * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa), 2)
                        );
            }

            return recargoPorMora;
        }

        public static string ObtenerConceptoMatrícula(Int32 idPlanPago, PagoWeb pago) //Pago pago)
        {
            string concepto = "";
            using (var db = new SMPorresEntities())
            {
                var cuotas = db.PagosWeb.Where(x => x.IdPlanPago == idPlanPago
                                            && x.NroCuota == 0
                                            && x.Estado != (short)EstadoPago.Baja)
                                            .OrderBy(x => x.Id);
                if (cuotas.Count() == 1)
                {
                    concepto = "Matrícula";
                }
                else
                {
                    short orden = 0;
                    foreach (var item in cuotas)
                    {
                        orden += 1;
                        if (item.Id == pago.Id)
                        {
                            concepto = "Matrícula Cuota Nº " + orden.ToString();
                        }
                    }
                }

            }
            return concepto;
        }

        public static int CantidadCuotasMatrícula(decimal idPlanPago)
        {
            int cc = 0;
            using (var db = new SMPorresEntities())
            {
                cc = db.PagosWeb.Where(x => x.IdPlanPago == idPlanPago && x.NroCuota == 0 && x.Estado == (short)EstadoPago.Impago)
                    .Count();
            }
            return cc;
        }
    }
}