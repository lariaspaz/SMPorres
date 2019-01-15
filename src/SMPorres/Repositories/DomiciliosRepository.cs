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
                }
            }
            return idDom;
        }

        

        internal static Domicilio ObtenerDomiciliosPorId(int idDomicilio)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Domicilios.Find(idDomicilio);
            }
        }

        internal static string ObtenerProvincia(int idProvincia)
        {
            using (var db = new Models.SMPorresEntities())
            {
                return (from p in db.Provincias where p.Id == idProvincia select p.Nombre).FirstOrDefault();
            }
        }

        internal static string ObtenerDepartamento(int idDepartamento)
        {
            using (var db = new Models.SMPorresEntities())
            {
                return (from d in db.Departamentos where d.Id == idDepartamento select d.Nombre).FirstOrDefault();
            }
        }
        internal static string ObtenerLocalidad(int idLocalidad)
        {
            using (var db = new Models.SMPorresEntities())
            {
                return (from l in db.Localidades where l.Id == idLocalidad select l.Nombre).FirstOrDefault();
            }
        }
        internal static string ObtenerBarrio(int idBarrio)
        {
            using (var db = new Models.SMPorresEntities())
            {
                return (from b in db.Barrios where b.Id == idBarrio select b.Nombre).FirstOrDefault();
            }
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
