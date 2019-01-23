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
