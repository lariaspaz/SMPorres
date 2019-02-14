using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class PagosRepository
    {
        private void Insertar(int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                var pp = db.PlanesPago.Find(idPlanPago);
                for (short i = 1; i <= Lib.Configuration.MaxCuotas; i++)
                {
                    var p = new Pago();
                    p.IdPlanPago = idPlanPago;
                    p.NroCuota = i;
                    p.ImporteCuota = pp.ImporteCuota;
                    db.Pagos.Add(p);
                }
            }
        }

        internal static void ActualizarCuotas(int idCurso, decimal importe, bool esMatrícula)
        {
            using (var db = new SMPorresEntities())
            {
                var pagos = from p in db.Pagos
                            where p.PlanesPago.IdCurso == idCurso &&
                                  !p.Fecha.HasValue
                            select p;
                if (esMatrícula)
                {
                    pagos = pagos.Where(m => m.NroCuota == 0);
                }
                else
                {
                    pagos = pagos.Where(m => m.NroCuota > 0);
                }
                foreach (var m in pagos)
                {
                    m.ImporteCuota = importe;
                }
                db.SaveChanges();
            }
        }
    }
}
