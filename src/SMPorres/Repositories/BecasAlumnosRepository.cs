using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class BecasAlumnosRepository
    {
        public static IEnumerable<Barrio> ObtenerBarriosPorLocalidad(int idLocalidad)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var deptos = db.Barrios.Where(b => b.IdLocalidad == idLocalidad).ToList()
                                .Select(
                                    b => new Barrio {
                                        Id = b.Id,
                                        Nombre = b.Nombre
                                    }
                                );
                return deptos.OrderBy(b => b.Nombre).ToList();
            }
        }

        public static BecaAlumno Insertar(int idAlumno, int idPago, short beca)
        {
            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    var id = db.BecasAlumnos.Any() ? db.BecasAlumnos.Max(ba => ba.Id) + 1 : 1;
                    var b = new BecaAlumno
                    {
                        Id = id,
                        IdAlumno = idAlumno,
                        PorcBeca = beca
                    };
                    db.BecasAlumnos.Add(b);
                    var p = db.Pagos.Find(idPago);
                    p.BecaAlumno = b;
                    db.SaveChanges();
                    trx.Commit();
                    return b;
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        internal static Barrio ObtenerBarrioPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Barrios.FirstOrDefault(b => b.Id == id);
            }
        }

        internal static BecaAlumno Actualizar(int id, short beca)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.BecasAlumnos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la beca con Id " + id);
                }
                var b = db.BecasAlumnos.Find(id);
                b.PorcBeca = beca;
                db.SaveChanges();
                return b;
            }
        }

        internal static void Eliminar(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Barrios.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el barrio con Id " + id);
                }
                var b = db.Barrios.Find(id);
                if (b.Domicilios.Any())
                {
                    throw new Exception("No se puede eliminar la localidad porque está relacionada a alumnos.");
                }
                db.Barrios.Remove(b);
                db.SaveChanges();
            }
        }
    }
}
