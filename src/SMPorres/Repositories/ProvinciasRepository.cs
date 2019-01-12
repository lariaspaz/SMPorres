using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class ProvinciasRepository
    {
        public static IEnumerable<Provincia> ObtenerProvincias()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var query = db.Provincias.ToList()
                                .Select(
                                    c => new Provincia {
                                        Id = c.Id,
                                        Nombre = c.Nombre
                                    }
                                 );
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }
    }
}
