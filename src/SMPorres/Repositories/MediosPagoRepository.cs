using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class MediosPagoRepository
    {
        public static IEnumerable<MedioPago> ObtenerMediosPago()
        {
            using (var db = new SMPorresEntities())
            {
                var deptos = db.MediosPago.ToList()
                                .Select(
                                    mp => new MedioPago
                                    {
                                        Id = mp.Id,
                                        Descripcion = mp.Descripcion
                                    }
                                );
                return deptos.OrderBy(b => b.Descripcion).ToList();
            }
        }
    }
}
