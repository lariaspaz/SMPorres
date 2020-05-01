using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class TasasMoraRepository
    {
        public static IList<TasaMora> ObtenerTasas()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from t in db.TasasMora select t)
                                .ToList()
                                .Select(
                                    t => new TasaMora
                                    {
                                        Id = t.Id,
                                        Tasa = t.Tasa,
                                        Desde = t.Desde,
                                        Hasta = t.Hasta,
                                        Estado = t.Estado
                                    });
                return (from t in query
                        orderby t.Desde ascending, t.Hasta descending, t.Tasa ascending
                        select t).ToList();
            }
        }

        public static TasaMora Insertar(double tasa, DateTime desde, DateTime hasta)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.TasasMora.Any() ? db.TasasMora.Max(t1 => t1.Id) + 1 : 1;
                var t = new TasaMora
                {
                    Id = id,
                    Tasa = tasa,
                    Desde = desde,
                    Hasta = hasta
                };
                db.TasasMora.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public static void Actualizar(decimal id, double tasa, DateTime desde, DateTime hasta, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.TasasMora.Any(t1 => t1.Id == id))
                {
                    throw new Exception("No existe la tasa con Id " + id);
                }
                var t = db.TasasMora.Find(id);
                t.Tasa = tasa;
                t.Desde = desde;
                t.Hasta = hasta;
                t.Estado = estado;
                db.SaveChanges();
            }
        }

        public static void Eliminar(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.TasasMora.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la tasa con Id " + id);
                }
                var t1 = db.TasasMora.Find(id);
                db.TasasMora.Remove(t1);
                db.SaveChanges();
            }
        }

        internal static TasaMora ObtenerTasaPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.TasasMora.Find(id);
            }
        }
    }
}
