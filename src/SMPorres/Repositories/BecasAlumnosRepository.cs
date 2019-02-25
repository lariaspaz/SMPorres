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
                var id = db.BecasAlumnos.Any() ? db.BecasAlumnos.Max(ba => ba.Id) + 1 : 1;
                var b = new BecaAlumno
                {
                    Id = id,
                    IdAlumno = idAlumno,
                    IdPago = idPago,
                    PorcBeca = beca
                };
                db.BecasAlumnos.Add(b);
                db.SaveChanges();
                return b;
            }
        }

        internal static Barrio ObtenerBarrioPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Barrios.FirstOrDefault(b => b.Id == id);
            }
        }

        internal static void Actualizar(int id, string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Barrios.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el barrio con Id " + id);
                }
                var barrio = db.Barrios.Find(id);
                if (db.Barrios.Any(b => b.Nombre.ToLower() == nombre.ToLower() &&
                        b.IdLocalidad == barrio.IdLocalidad))
                {
                    throw new Exception("Ya existe un barrio con este nombre en esta localidad.");
                }
                barrio.Nombre = nombre;
                db.SaveChanges();
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
