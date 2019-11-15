using EnvioEMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvioEMail.Repositories
{
    class CarrerasRepository
    {
        public static IList<Carreras> ObtenerCarreras()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Carreras select c)
                                .ToList()
                                .Select(
                                    c => new Carreras
                                    {
                                        Id = c.Id,
                                        Nombre = c.Nombre,
                                        Duracion = c.Duracion,
                                        Estado = c.Estado,
                                        FechaEstado = c.FechaEstado
                                    });
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }
    }
}
