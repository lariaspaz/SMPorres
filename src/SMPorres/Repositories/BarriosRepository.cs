using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class BarriosRepository
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

        public static Barrio Insertar(int idLocalidad, string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.Barrios.Any(b => b.Nombre.ToLower() == nombre.ToLower() &&
                        b.IdLocalidad == idLocalidad))
                {
                    throw new Exception("Ya existe un barrio con este nombre en esta localidad.");
                }

                var id = db.Barrios.Any() ? db.Barrios.Max(b => b.Id) + 1 : 1;
                var barrio = new Barrio
                {
                    Id = id,
                    IdLocalidad = idLocalidad,
                    Nombre = nombre
                };
                db.Barrios.Add(barrio);
                db.SaveChanges();
                return barrio;
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
