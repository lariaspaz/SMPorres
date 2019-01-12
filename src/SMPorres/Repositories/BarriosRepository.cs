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
    }
}
