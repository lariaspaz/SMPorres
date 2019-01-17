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
                var query = (from c in db.ItemsMenus where c.IdPadre==0 select c)
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
