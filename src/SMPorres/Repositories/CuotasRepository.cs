using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class CuotasRepository
    {
        public static IList<Cuota> ObtenerCuotas()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Cuotas select c)
                                .ToList()
                                .Select(
                                    c => new Cuota
                                    {
                                        Id = c.Id,
                                        NroCuota = c.NroCuota,
                                        VtoCuota = c.VtoCuota,
                                        CicloLectivo = c.CicloLectivo
                                    });
                return query.OrderBy(c => c.CicloLectivo).ThenBy(c => c.NroCuota).ToList();
            }
        }

        public static Cuota Insertar(short nroCuota, DateTime vtoCuota, short cicloLectivo)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Cuotas.Any() ? db.Cuotas.Max(c1 => c1.Id) + 1 : 1;
                var c = new Cuota
                {
                    Id = id,
                    NroCuota = nroCuota,
                    VtoCuota = vtoCuota,
                    CicloLectivo = cicloLectivo
                };
                db.Cuotas.Add(c);
                db.SaveChanges();
                return c;
            }
        }

        public static void Actualizar(int id, short nroCuota, DateTime vtoCuota, short cicloLectivo)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Cuotas.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe la cuota {0} - Id: {1}", nroCuota, id));
                }
                var c = db.Cuotas.Find(id);
                c.NroCuota = nroCuota;
                c.VtoCuota = vtoCuota;
                c.CicloLectivo = cicloLectivo;
                db.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Cuotas.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la cuota con Id " + id);
                }
                var c = db.Cuotas.Find(id);
                db.Cuotas.Remove(c);
                db.SaveChanges();
            }
        }

        internal static Cuota ObtenerCuotaPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Cuotas.FirstOrDefault(c => c.Id == id);
            }
        }

        //public static short MáximaCuotaVencida
        //{
        //    get
        //    {
        //        short c = 0;
        //        using (var db = new SMPorresEntities())
        //        {
        //            //if (db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).Max(x => x.NroCuota) > 0)
        //            if (db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).FirstOrDefault() != null )
        //            {
        //                c = db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).Max(x => x.NroCuota);
        //            }
        //            else
        //            {
        //                c = 1; //primer cuota
        //            }
        //        }

        //        return c;
        //    }
        //}

        public static Cuota PróximaCuota
        {
            get
            {
                DateTime hoy = DateTime.Today;
                using (var db = new SMPorresEntities())
                {

                    //return db.Cuotas.Where(x => x.VtoCuota >= DateTime.Today).FirstOrDefault();
                    var query = (from c in db.Cuotas
                                 where c.VtoCuota >= DateTime.Today
                                 orderby c.VtoCuota
                                 select c)
                                .FirstOrDefault();
                    return query;
                }
            }
        }

        public static List<short> CuotasImpagas(Alumno alumno)
        {
            using (var db = new SMPorresEntities())
            {
                List<short> c = new List<short>();
                var query = (from pp in db.PlanesPago
                             join p in db.Pagos on pp.Id equals p.IdPlanPago
                             where pp.Estado == 1 && //Planes de pago activos
                                 p.ImportePagado == null &&// Cuota impaga
                                //p.NroCuota <= CuotasRepository.MáximaCuotaVencida &&
                                 p.FechaVto <= System.DateTime.Today &&
                                 pp.IdAlumno == alumno.Id
                             orderby p.NroCuota
                             select p.NroCuota
                             )
                             .ToList();
                foreach (var item in query)
                {
                    bool has = c.Any(x => x == item);
                    if (!has)
                    {
                        c.Add(item);
                    }
                    
                }
                return c;
            }
        }

        public static int CuotasVencidasImpagas(Alumno alumno)
        {
            using (var db = new SMPorresEntities())
            {
                int c = 0;
                var query = (from pp in db.PlanesPago
                             join p in db.Pagos on pp.Id equals p.IdPlanPago
                             where pp.Estado == 1 && //Planes de pago activos
                                 p.ImportePagado == null &&// Cuota impaga
                                 p.FechaVto <= System.DateTime.Today &&
                                 //p.NroCuota <= CuotasRepository.MáximaCuotaVencida &&
                                 pp.IdAlumno == alumno.Id
                             orderby pp.Id
                             select new PermisoExamen
                             {
                                 Situación = "s",
                                 Texto = "t",
                                 Cuotas = 0
                             })
                             .ToList();

                var pexamen = (from t in query
                               group t by new { t.Situación, t.Texto }
                               into g
                               select new PermisoExamen
                               {
                                   Situación = g.Key.Situación,
                                   Texto = g.Key.Texto,
                                   Cuotas = g.Count()
                               });
                foreach (var item in pexamen)
                {
                    c += 1; // item.Cuotas;
                }
                return c;

            }
        }



    }
}
