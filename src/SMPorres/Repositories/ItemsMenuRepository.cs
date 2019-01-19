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

        public static IList<ItemsMenu> ObtenerPadresItemsMenu()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.ItemsMenus where c.IdPadre == 0 select c)
                                .ToList()
                                .Select(
                                    c => new ItemsMenu
                                    {
                                        Id = c.Id,
                                        IdPadre = c.IdPadre,
                                        Nombre = c.Nombre,
                                        Descripcion = c.Descripcion
                                    });
                return query.OrderBy(c => c.IdPadre).ToList();
            }
        }

        public static ItemsMenu ObtenerItemMenuPorDescripcion(string itemMenu)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.ItemsMenus where c.Descripcion == itemMenu select c)
                                .ToList()
                                .Select(
                                    c => new ItemsMenu
                                    {
                                        Id = c.Id,
                                        IdPadre = c.IdPadre,
                                        Nombre = c.Nombre,
                                        Descripcion = c.Descripcion
                                    }).FirstOrDefault();
                return query;
            }

        }

        public static ItemsMenu ObtenerItemMenuPorId(int idItemMenu)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.ItemsMenus where c.Id == idItemMenu select c)
                                .ToList()
                                .Select(
                                    c => new ItemsMenu
                                    {
                                        Id = c.Id,
                                        IdPadre = c.IdPadre,
                                        Nombre = c.Nombre,
                                        Descripcion = c.Descripcion
                                    }).FirstOrDefault();
                return query;
            }

        }

        public static IList<ItemsMenu> ObtenerHijosPorId(int padre)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.ItemsMenus where c.IdPadre == padre select c)
                                .ToList()
                                .Select(
                                    c => new ItemsMenu
                                    {
                                        Id = c.Id,
                                        IdPadre = c.IdPadre,
                                        Nombre = c.Nombre,
                                        Descripcion = c.Descripcion
                                    });
                return query.OrderBy(c => c.IdPadre).ToList();
            }
        }

        public class CategoriaEntity
        {
            public int IdCategoria { get; set; }
            public int? IdCategoriaPadre { get; set; }
            public string Descripcion { get; set; }
            public short Posicion { get; set; }
        }

        public class CategoriaJerarquica
        {
            public int IdCategoria { get; set; }
            public string Descripcion { get; set; }
            public List<CategoriaJerarquica> CategoriaHija { get; set; }
        }

        public static List<CategoriaJerarquica> ObtenerCategoriarJerarquia()
        {
            using (var db = new Models.SMPorresEntities())
            {
                List<ItemsMenu> itemsMenu = ObtenerItemsMenu().ToList();

                List<CategoriaJerarquica> query = (from item in itemsMenu
                                                   where item.IdPadre == 0
                                                   select new CategoriaJerarquica
                                                   {
                                                       IdCategoria = item.Id,
                                                       Descripcion = item.Descripcion,
                                                       CategoriaHija = ObtenerHijos(item.Id, itemsMenu)
                                                   }).ToList();

                return query;
            }

        }

        private static List<CategoriaJerarquica> ObtenerHijos(int idCategoria, List<ItemsMenu> items)
        {
            List<CategoriaJerarquica> query = (from item in items
                                               let tieneHijos = items.Where(o => o.IdPadre == item.Id).Any()
                                               where item.Id == idCategoria
                                               select new CategoriaJerarquica
                                               {
                                                   IdCategoria = item.Id,
                                                   Descripcion = item.Descripcion,
                                                   CategoriaHija = tieneHijos ? ObtenerHijos(item.Id, items) : null
                                               }).ToList();

            return query;
        }
    }
}
