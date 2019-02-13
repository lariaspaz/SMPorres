using SMPorres.Lib;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class CarrerasRepository
    {
        public static IList<Carrera> ObtenerCarreras()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Carreras select c)
                                .ToList()
                                .Select(
                                    c => new Carrera
                                    {
                                        Id = c.Id,
                                        Nombre = c.Nombre,
                                        Duracion = c.Duracion,
                                        Estado = c.Estado,
                                        FechaEstado = c.FechaEstado
                                    });
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }

        public static Carrera Insertar(string nombre, short duración, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Carreras.Any() ? db.Carreras.Max(c1 => c1.Id) + 1 : 1;
                var c = new Carrera
                {
                    Id = id,
                    Nombre = nombre,
                    Duracion = duración,
                    Estado = estado,
                    FechaEstado = Configuration.CurrentDate
                };
                db.Carreras.Add(c);
                db.SaveChanges();
                return c;
            }
        }

        public static void Actualizar(decimal id, string nombre, short duración, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Carreras.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe la carrera {0} - {1}", id, nombre));
                }
                var c = db.Carreras.Find(id);
                c.Nombre = nombre;
                c.Duracion = duración;
                if (c.Estado != estado)
                {
                    c.Estado = estado;
                    c.FechaEstado = Configuration.CurrentDate;
                }
                db.SaveChanges();
            }
        }

        public static void Eliminar(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Carreras.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la carrera con Id " + id);
                }
                var c = db.Carreras.Find(id);
                if (c.Cursos.Any())
                {
                    throw new Exception("No se puede eliminar la carrera: hay cursos relacionados.");
                }
                db.Carreras.Remove(c);
                db.SaveChanges();
            }
        }

        internal static Carrera ObtenerCarreraPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Carreras.Find(id);
            }
        }
    }
}
