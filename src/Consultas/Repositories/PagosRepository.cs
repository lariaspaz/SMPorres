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

        public void Eliminar(SMPorresEntities db, Models.WebServices.Pago pago)
        {
            var p = db.PagosWeb.Find(pago.Id);
            if ((p.ImportePagado ?? 0) == 0)
            {
                db.PagosWeb.Remove(p);
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

        internal void EliminarPagosNoReferenciados(SMPorresEntities db, List<CursoAlumno> cursosAlumnos)
        {
            foreach (var item in cursosAlumnos) //--> cuotas recibidas desde web service
            {
                var pdb = db.PagosWeb.Where(x => x.IdCursoAlumno == item.Id);   //--> todas cuotas del alumno en db
                foreach (var i in pdb)
                {
                    var EliminarPago = db.PagosWeb.Find(i.Id);
                    db.PagosWeb.Remove(EliminarPago);
                    db.SaveChanges();
                }
            }
        }

        public int ObtenerIdPrimeraCuotaImpaga()
        {
            using (var db = new SMPorresEntities())
            {
                var p2 = (from p in db.PagosWeb
                          where p.CursoAlumnoWeb.AlumnoWeb.Id == Session.CurrentUserId &&
                                p.Fecha == null
                          select p).FirstOrDefault();
                return (p2 == null) ? 0 : p2.Id;
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
            var impBecado = pago.ImporteCuota - pago.ImporteBeca ?? 0;
            if (fechaCompromiso <= pago.FechaVto)
            {
                if (fechaCompromiso <= pago.FechaVtoPagoTermino)
                {
                    totalAPagar = impBecado - pago.ImportePagoTermino ?? 0;
                }
                else
                {
                    totalAPagar = impBecado;
                }
            }
            else
            {
                var porcRecargo = (new ConfiguracionRepository().ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
                var díasAtraso = Math.Truncate((fechaCompromiso - pago.FechaVto).TotalDays);
                var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);

                decimal recargoPorMora = 0;
                if (pago.TipoBeca == (byte)TipoBeca.AplicaSiempre)
                {
                    recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                }
                else
                {
                    recargoPorMora = Math.Round(pago.ImporteCuota * porcRecargoTotal, 2);
                    pago.ImporteBeca = 0;
                }
                pago.ImporteRecargo = recargoPorMora;
                totalAPagar = pago.ImporteCuota - (pago.ImporteBeca ?? 0) + recargoPorMora;
            }

            pago.ImportePagado = totalAPagar;
            return pago;
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