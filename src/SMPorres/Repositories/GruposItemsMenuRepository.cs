using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    class GruposItemsMenuRepository
    {
        internal static IList<ItemsMenu> ObtenerItemsMenuPorGrupoId(int idGrupo)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (

                    //from u in db.Usuarios
                    //join gu in db.GruposUsuarios on u.Id equals gu.IdUsuario
                    //where u.Estado == 1 && gu.IdGrupo == idGrupo
                    //select u)

                            from it in db.ItemsMenus
                             join gim in db.GruposItemsMenus on it.Id equals gim.IdItemMenu
                             where gim.IdGrupo == idGrupo
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
                return query.OrderBy(u => u.Nombre).ToList();
            }
        }

        internal static void Insertar(int idGrupo, int idItemMenu)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.GruposItemsMenus.Any() ? db.GruposItemsMenus.Max(g1 => g1.Id) + 1 : 1;
                var gu = new GruposItemsMenu { Id = id, IdGrupo = idGrupo, IdItemMenu = idItemMenu };
                db.GruposItemsMenus.Add(gu);
                db.SaveChanges();
            }
        }

        internal static void Eliminar(int idGrupo, int idItemMenu)
        {
            using (var db = new SMPorresEntities())
            {
                var gu = db.GruposItemsMenus.FirstOrDefault(t => t.IdGrupo == idGrupo && t.IdItemMenu == idItemMenu);
                if (gu == null) return;
                db.GruposItemsMenus.Remove(gu);
                db.SaveChanges();
            }
        }



    }
}
