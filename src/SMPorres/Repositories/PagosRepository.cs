using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class PagosRepository
    {
        private void Insertar(int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                var pp = db.PlanesPago.Find(idPlanPago);
                for (short i = 1; i <= Lib.Configuration.MaxCuotas; i++)
                {
                    var p = new Pago();
                    p.IdPlanPago = idPlanPago;
                    p.NroCuota = i;
                    p.ImporteCuota = pp.ImporteCuota;
                    db.Pagos.Add(p);
                }
            }
        }
    }
}
