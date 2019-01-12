using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    class DomiciliosRepository
    {
        public static int ObtenerIdDomicilio(int provincia, int departamento, int localidad, int barrio)
        {
            int idDom = 0;
            using (var db = new Models.SMPorresEntities())
            {
                idDom = (from d in db.Domicilios
                         where
                            d.IdProvincia == provincia &
                            d.IdDepartamento == departamento &
                            d.IdLocalidad == localidad &
                            d.IdBarrio == barrio
                         select d.Id).FirstOrDefault();

                if (idDom <= 0)
                {
                    idDom = Insertar(provincia, departamento, localidad, barrio);
                    //idDom = db.Domicilios.DefaultIfEmpty().Max(d1 => d1 == null ? 0 : d1.Id) + 1;
                }
            }
            return idDom;
        }

        public static int Insertar(int provincia, int departamento, int localidad, int barrio)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Domicilios.Any() ? db.Domicilios.Max(a1 => a1.Id) + 1 : 1;
                var a = new Domicilio
                {
                    Id = id,
                    IdProvincia = provincia,
                    IdDepartamento = departamento,
                    IdLocalidad = localidad,
                    IdBarrio = barrio
                };
                db.Domicilios.Add(a);
                db.SaveChanges();
                return a.Id;
            }
        }
     }
}
