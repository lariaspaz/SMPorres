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
                            where p.PlanPago.IdCurso == idCurso &&
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

        internal static List<Pago> ObtenerPagos(int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from p in db.Pagos where p.IdPlanPago == idPlanPago select p)
                            .ToList()
                            .Select(
                                p => new Pago
                                {
                                    IdPago = p.IdPago,
                                    NroCuota = p.NroCuota,
                                    ImporteCuota = p.ImporteCuota,
                                    Fecha = p.Fecha
                                }
                            );                    
                return query.OrderBy(p => p.NroCuota).ToList();
            }
        }

        internal static Pago ObtenerPago(int idPago)
        {
            using (var db = new SMPorresEntities())
            {
                var p = db.Pagos.Find(idPago);
                db.Entry(p).Reference(p1 => p1.PlanPago).Load();
                db.Entry(p.PlanPago).Reference(pp => pp.Alumno).Load();
                db.Entry(p.PlanPago.Alumno).Reference(a => a.TipoDocumento).Load();
                db.Entry(p.PlanPago).Reference(pp => pp.Curso).Load();
                db.Entry(p.PlanPago.Curso).Reference(c => c.Carrera).Load();
                return p;
            }
        }
    }
}
