﻿using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
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
    }
}
