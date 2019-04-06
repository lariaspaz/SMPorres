using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class ConfiguracionRepository
    {
        public static Configuracion ObtenerConfiguracion()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var conf = db.Configuraciones.ToList()
                                .Select(
                                    c => new Configuracion
                                    {
                                        Id = c.Id,
                                        DescuentoPagoTermino = c.DescuentoPagoTermino,
                                        InteresPorMora = c.InteresPorMora,
                                        CicloLectivo = c.CicloLectivo,
                                        EndpointAddress = c.EndpointAddress,
                                        DiasVtoPagoTermino = c.DiasVtoPagoTermino
                                    }
                                );
                if (conf.Any())
                {
                    return conf.FirstOrDefault();
                }
                else
                {
                    return new Configuracion { CicloLectivo = (short)DateTime.Now.Year };
                }
            }
        }

        public static void Actualizar(double descuentoPagoTermino, double interesPorMora, short cicloLectivo,
            short diasVtoPagoTermino, string endpointAddress)
        {
            using (var db = new SMPorresEntities())
            {
                var conf = db.Configuraciones.Any() ? db.Configuraciones.First() : new Configuracion();
                conf.DescuentoPagoTermino = descuentoPagoTermino;
                conf.InteresPorMora = interesPorMora;
                conf.CicloLectivo = cicloLectivo;
                conf.DiasVtoPagoTermino = diasVtoPagoTermino;
                conf.EndpointAddress = endpointAddress;
                if (!db.Configuraciones.Any())
                {
                    conf.Id = 1;
                    db.Configuraciones.Add(conf);
                }
                db.SaveChanges();
            }
        }
    }
}
