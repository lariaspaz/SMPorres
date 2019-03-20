using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class PagosRepository
    {
        public void Actualizar(int idCursoAlumno, Models.WebServices.Pago pago)
        {
            using (var db = new SMPorres_DevEntities())
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
                p.NroCuota = (short) pago.NroCuota;
                p.ImporteCuota = pago.ImporteCuota;
                p.ImporteBeca = pago.ImporteBeca;
                p.ImporteRecargo = pago.ImporteRecargo;
                p.Fecha = pago.Fecha;
                p.FechaVto = pago.FechaVto;
                if (insertar)
                {
                    db.PagoWebs.Add(p);
                }
                db.SaveChanges();
            }
        }
    }
}