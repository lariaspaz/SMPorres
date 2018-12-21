using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class CarrerasRepository
    {
        public static IList<Carrera> ObtenerCarreras()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from c in db.Carreras select c)
                                .ToList()
                                .Select(
                                    c => new Carrera
                                    {
                                        Id = c.Id,
                                        Nombre = c.Nombre,
                                        Duracion = c.Duracion,
                                        Importe1Vto = c.Importe1Vto,
                                        Importe2Vto = c.Importe2Vto,
                                        Importe3Vto = c.Importe3Vto,
                                        Estado = c.Estado,
                                        FechaEstado = c.FechaEstado
                                    });
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }

        public static Carrera Insertar(string nombre, short duración, decimal importe1Vto, decimal importe2Vto, 
            decimal importe3Vto, short estado)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Carreras.Max(c1 => c1.Id) + 1;
                var c = new Carrera {
                    Nombre = nombre,
                    Duracion = duración,
                    Importe1Vto = importe1Vto,
                    Importe2Vto = importe2Vto,
                    Importe3Vto = importe3Vto,
                    Estado = estado
                };
                db.Carreras.Add(c);
                db.SaveChanges();
                return c;
            }
        }
    }
}
