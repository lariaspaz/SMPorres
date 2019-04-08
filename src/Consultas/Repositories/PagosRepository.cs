using Consultas.Lib;
using Consultas.Lib.Security;
using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                                  p.CursoAlumnoWeb.AlumnoWeb.Id == Session.CurrentUserId
                          select p;
                return qry.ToList();
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

        public PagoWeb ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
        {
            var pago = new PagosRepository().ObtenerPago(idPago);

            pago.ImporteRecargo = 0;
            pago.ImportePagado = pago.ImporteCuota;

            var totalAPagar = (decimal)0;
            if (fechaCompromiso <= pago.FechaVto)
            {
                if (fechaCompromiso <= pago.FechaVtoPagoTermino)
                {
                    totalAPagar = pago.ImporteCuota - pago.ImporteBeca ?? 0 - pago.ImportePagoTermino ?? 0;
                }
                else
                {
                    totalAPagar = pago.ImporteCuota - pago.ImporteBeca ?? 0;
                }
            }
            else
            {
                var porcRecargo = (new ConfiguracionRepository().ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
                var díasAtraso = Math.Truncate((fechaCompromiso - pago.FechaVto).TotalDays);
                var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                var impBecado = pago.ImporteCuota - pago.ImporteBeca ?? 0;
                var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);

                totalAPagar = pago.ImporteCuota - (pago.ImporteBeca ?? 0) + recargoPorMora;

                pago.ImporteRecargo = recargoPorMora;
            }

            pago.ImportePagado = totalAPagar;
            return pago;
        }
    }
}