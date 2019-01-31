using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class CursosRepository
    {
        public static IList<Curso> ObtenerCursosPorIdCarrera(int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Cursos where c.IdCarrera == idCarrera select c)
                                .ToList()
                                .Select(
                                    c => new Curso
                                    {
                                        Id = c.Id,
                                        Nombre = c.Nombre,
                                        IdCarrera = c.IdCarrera,
                                        ImporteCuota = c.ImporteCuota
                                    });
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }

        public static Curso Insertar(string nombre, int idCarrera, decimal importeCuota)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Cursos.Any() ? db.Cursos.Max(c1 => c1.Id) + 1 : 1;
                var c = new Curso
                {
                    Id = id,
                    Nombre = nombre,
                    IdCarrera = idCarrera,
                    ImporteCuota = importeCuota
                };
                db.Cursos.Add(c);
                db.SaveChanges();
                return c;
            }
        }

        public static void Actualizar(int id, string nombre, int idCarrera, decimal importeCuota)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Cursos.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe el curso {0} - {1}", id, nombre));
                }
                var c = db.Cursos.Find(id);
                c.Nombre = nombre;
                c.IdCarrera = idCarrera;
                c.ImporteCuota = importeCuota;
                db.SaveChanges();
            }
        }

        public static void Eliminar(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Cursos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el curso con Id " + id);
                }
                var c = db.Cursos.Find(id);
                if (c.CursosAlumnos.Any())
                {
                    throw new Exception("No puede eliminar el curso porque tiene alumnos relacionados.");
                }
                db.Cursos.Remove(c);
                db.SaveChanges();
            }
        }

        internal static Curso ObtenerCursoPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Cursos.Find(id);
            }
        }
    }
}
