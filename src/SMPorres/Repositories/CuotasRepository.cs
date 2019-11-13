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
                                        VtoCuota = c.VtoCuota
                                    });
                return query.OrderBy(c => c.NroCuota).ToList();
            }
        }

        public static Cuota Insertar(short nroCuota, DateTime vtoCuota)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Cuotas.Any() ? db.Cuotas.Max(c1 => c1.Id) + 1 : 1;
                var c = new Cuota
                {
                    Id = id,
                    NroCuota = nroCuota,
                    VtoCuota = vtoCuota
                };
                db.Cuotas.Add(c);
                db.SaveChanges();
                return c;
            }
        }

        public static void Actualizar(int id, short nroCuota, DateTime vtoCuota)
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

        public static short MáximaCuotaVencida
        {
            get
            {
                using (var db = new SMPorresEntities())
                {
                    return db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).Max(x => x.NroCuota);
                }
            }
        }

        public static DateTime ÚltimoVencimiento()
        {
            using (var db = new SMPorresEntities())
            {
                //return Convert.ToDateTime( db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today) );
                return db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).Max(x => x.VtoCuota);
            }
        }
    }
}
