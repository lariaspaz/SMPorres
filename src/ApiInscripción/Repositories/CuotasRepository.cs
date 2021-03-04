using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    public class CuotasRepository
    {
        public static IList<Cuota> ObtenerCuotas()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Cuotas select c)
                                .ToList()
                                .Select(
                                    c => new Cuota
                                    {
                                        Id = c.Id,
                                        NroCuota = c.NroCuota,
                                        VtoCuota = c.VtoCuota,
                                        CicloLectivo = c.CicloLectivo
                                    });
                return query.OrderBy(c => c.NroCuota).ToList();
            }
        }

        public static IList<Cuota> ObtenerCuotasActuales()
        {
            var cl = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
            return ObtenerCuotas().Where(c => c.CicloLectivo == cl).ToList();
        }
    }
}
