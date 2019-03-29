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
            var p = db.PagoWebs.Find(pago.Id);
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
            if (insertar)
            {
                db.PagoWebs.Add(p);
            }
            db.SaveChanges();
        }

        public IEnumerable<PagoWeb> ObtenerPagos(int idCursoAlumno)
        {
            using (var db = new SMPorresEntities())
            {
                return db.PagoWebs.Where(p => p.IdCursoAlumno == idCursoAlumno).ToList();
            }
        }
    }
}