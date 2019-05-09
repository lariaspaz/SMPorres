using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    class ItemsMenuRepository
    {
        public static void Actualizar(string nombre, string descripción, string nombrePadre)
        {
            using (var db = new SMPorresEntities())
            {
                var padre = ObtenerMenuItem(nombrePadre) ?? new ItemsMenu { Id = 0 };
                if (!db.ItemsMenus.Any(im => im.Nombre == nombre && padre.Nombre == nombrePadre))
                {
                    var im = new ItemsMenu();
                    im.Id = db.ItemsMenus.Any() ? db.ItemsMenus.Max(c1 => c1.Id) + 1 : 1;
                    im.Nombre = nombre;
                    im.Descripcion = descripción;
                    im.IdPadre = padre.Id;
                    db.ItemsMenus.Add(im);
                }
                else
                {
                    var im = db.ItemsMenus.First(im1 => im1.Nombre == nombre);
                    im.Nombre = nombre;
                    im.Descripcion = descripción;
                    im.IdPadre = padre.Id;
                }
                db.SaveChanges();
            }
        }

        public static void EliminarItemsInexistentes(IList<string> menuItemsNames)
        {
            using (var db = new SMPorresEntities())
            {
                foreach (var item in db.ItemsMenus)
                {
                    if (!menuItemsNames.Contains(item.Nombre))
                    {
                        db.UsuariosItemsMenus.RemoveRange(db.UsuariosItemsMenus.Where(uim => uim.IdItemMenu == item.Id));
                        db.ItemsMenus.Remove(item);
                    }
                }
                db.SaveChanges();
            }
        }

        private static ItemsMenu ObtenerMenuItem(string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                return db.ItemsMenus.FirstOrDefault(im => im.Nombre == nombre);
            }
        }

        public static IList<ItemsMenu> ObtenerItemsMenu()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.ItemsMenus select c)
                                .ToList()
                                .Select(
                                    c => new ItemsMenu
                                    {
                                        Id = c.Id,
                                        IdPadre = c.IdPadre,
                                        Nombre = c.Nombre,
                                        Descripcion = c.Descripcion
                                    });
                return query.OrderBy(c => c.Id).ToList();
            }
        }

        public static IList<ItemsMenu> ObtenerItemsMenu(int idusuario)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from ui in db.UsuariosItemsMenus
                             join ime in db.ItemsMenus
                             on ui.IdItemMenu equals ime.Id
                             where ui.IdUsuario == idusuario
                             select ime)
                             .ToList()
                             .Select(
                                ime => new ItemsMenu
                                {
                                    Id = ime.Id,
                                    IdPadre = ime.IdPadre,
                                    Nombre = ime.Nombre,
                                    Descripcion = ime.Descripcion
                                });
                return query.OrderBy(q => q.Id).ToList();
            }
        }

        
    }
}
