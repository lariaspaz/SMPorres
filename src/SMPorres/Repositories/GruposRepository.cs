using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    class GruposRepository
    {
        public static IList<Grupos> ObtenerGrupoUsuarios()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from g in db.Grupos select g)
                                .ToList()
                                .Select(
                                    g => new Grupos
                                    {
                                        Id = g.Id,
                                        Descripcion = g.Descripcion,
                                        Estado = g.Estado
                                    });
                return query.OrderBy(g => g.Descripcion).ToList();
            }
        }

        public static Grupos Insertar(string descripción, byte estado)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Grupos.Any() ? db.Grupos.Max(c1 => c1.Id) + 1 : 1;
                var g = new Grupos
                {
                    Id = id,
                    Descripcion = descripción,
                    Estado = estado
                };
                db.Grupos.Add(g);
                db.SaveChanges();
                return g;
            }
        }

        internal static Grupos ObtenerGrupoPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Grupos.Find(id);
            }
        }

        public static void Actualizar(decimal id, string descripción, byte estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Grupos.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe el grupo {0} - {1}", id, descripción));
                }
                var c = db.Grupos.Find(id);
                c.Descripcion = descripción;
                if (c.Estado != estado)
                {
                    c.Estado = estado;
                }
                db.SaveChanges();
            }
        }
    }
}
