using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class TiposDocumentoRepository
    {
        public static IEnumerable<TipoDocumento> ObtenerTiposDocumento()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var deptos = db.TiposDocumento.ToList()
                             .Select(
                                td => new TipoDocumento {
                                  Id = td.Id,
                                  Descripcion = td.Descripcion
                                }
                             );
                return deptos.OrderBy(b => b.Descripcion).ToList();
            }
        }
    }
}
