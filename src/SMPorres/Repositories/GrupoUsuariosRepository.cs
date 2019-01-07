using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    class GrupoUsuariosRepository
    {
        public static IList<GrupoUsuario> ObtenerGrupoUsuarios()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from g in db.GrupoUsuarios select g)
                                .ToList()
                                .Select(
                                    g => new GrupoUsuario
                                    {
                                        Id = g.Id,
                                        Descripcion = g.Descripcion,
                                        Estado = g.Estado
                                    });
                return query.OrderBy(g => g.Descripcion).ToList();
            }
        }

        public static GrupoUsuario Insertar(string descripción, byte estado)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.GrupoUsuarios.Any() ? db.GrupoUsuarios.Max(c1 => c1.Id) + 1 : 1;
                var g = new GrupoUsuario
                {
                    Id = id,
                    Descripcion = descripción,
                    Estado = estado
                };
                db.GrupoUsuarios.Add(g);
                db.SaveChanges();
                return g;
            }
        }

        internal static GrupoUsuario ObtenerGrupoUsuarioPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.GrupoUsuarios.Find(id);
            }
        }

        public static void Actualizar(decimal id, string descripción, byte estado)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.GrupoUsuarios.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe el grupo {0} - {1}", id, descripción));
                }
                var c = db.GrupoUsuarios.Find(id);
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
