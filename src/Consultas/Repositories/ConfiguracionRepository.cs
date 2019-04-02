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
    }
}