using EnvioEMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvioEMail.Repositories
{
    class CuotasRepository
    {
        public static IList<Cuotas> ObtenerCuotas()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Cuotas select c)
                                .ToList()
                                .Select(
                                    c => new Cuotas
                                    {
                                        Id = c.Id,
                                        Cuota = c.Cuota,
                                        VtoCuota = c.VtoCuota
                                    });
                return query.OrderBy(c => c.Cuota).ToList();
            }
        }

        public static short MáximaCuotaVencida
        {
            get
            {
                using (var db = new SMPorresEntities())
                {
                    return db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).Max(x => x.Cuota);
                }
            }
        }

        public static DateTime ÚltimoVencimiento()
        {
            using (var db = new SMPorresEntities())
            {
                //return Convert.ToDateTime( db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today) );
                return db.Cuotas.Where(x => x.VtoCuota <= DateTime.Today).Max(x => x.VtoCuota);
            }
        }
    }
}
