using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    class UsuariosItemsMenuRepository
    {
        internal static IList<ItemsMenu> ObtenerItemsMenuPorUsuarioId(int idUsuario)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (
                            from it in db.ItemsMenus
                            join uim in db.UsuariosItemsMenus on it.Id equals uim.IdItemMenu
                            where uim.IdUsuario == idUsuario
                            select it)
                             .ToList()
                                .Select(
                                    u => new ItemsMenu
                                    {
                                        Id = u.Id,
                                        IdPadre = u.IdPadre,
                                        Nombre = u.Nombre,
                                        Descripcion = u.Descripcion
                                    });
                return query.OrderBy(u => u.Id).ToList();
            }
        }

        internal static void Insertar(int idUsuario, int idItemMenu)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.UsuariosItemsMenus.Any() ? db.UsuariosItemsMenus.Max(g1 => g1.Id) + 1 : 1;
                var uu = new UsuariosItemsMenu { Id = id, IdUsuario = idUsuario, IdItemMenu = idItemMenu };
                db.UsuariosItemsMenus.Add(uu);
                db.SaveChanges();
            }
        }

        internal static void Eliminar(int idUsuario, int idItemMenu)
        {
            using (var db = new SMPorresEntities())
            {
                var uu = db.UsuariosItemsMenus.FirstOrDefault(t => t.IdUsuario == idUsuario && t.IdItemMenu == idItemMenu);
                if (uu == null) return;
                db.UsuariosItemsMenus.Remove(uu);
                db.SaveChanges();
            }
        }
    }
}
