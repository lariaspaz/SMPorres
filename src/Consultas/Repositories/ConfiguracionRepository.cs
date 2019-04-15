using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class ConfiguracionRepository
    {
        public ConfiguracionWeb ObtenerConfiguracion()
        {
            using (var db = new SMPorresEntities())
            {
                return db.ConfiguracionesWeb.FirstOrDefault();
            }
        }

        public void Actualizar(double interésPorMora)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.ConfiguracionesWeb.Any())
                {
                    var conf = db.ConfiguracionesWeb.First();
                    conf.InteresPorMora = interésPorMora;
                }
                else
                {
                    var conf = new ConfiguracionWeb();
                    conf.Id = 1;
                    conf.InteresPorMora = interésPorMora;
                    db.ConfiguracionesWeb.Add(conf);
                }
                db.SaveChanges();
            }
        }
    }
}