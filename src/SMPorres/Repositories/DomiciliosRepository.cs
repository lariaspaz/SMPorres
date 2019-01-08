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
        public static Int32 obtenerIdDomicilio(Int32 provincia, Int32 departamento, Int32 localidad, Int32 barrio)
        {
            Int32 idDom = 0;
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

        public static Int32 Insertar(Int32 provincia, Int32 departamento, Int32 localidad, Int32 barrio)
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

        public static Departamento InsertarDepartamento(Int32 idP, string departamento)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Departamentos.Any() ? db.Departamentos.Max(d1 => d1.Id) + 1 : 1;
                var d = new Departamento
                {
                    Id = id,
                    IdProvincia = idP,
                    Nombre = departamento
                };
                db.Departamentos.Add(d);
                db.SaveChanges();
                return d;
            }
        }

        public static Localidad InsertarLocalidad(Int32 idDepartamento, string localidad)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Localidades.Any() ? db.Localidades.Max(l1 => l1.Id) + 1 : 1;
                var l = new Localidad
                {
                    Id = id,
                    IdDepartamento = idDepartamento,
                    Nombre = localidad
                };
                db.Localidades.Add(l);
                db.SaveChanges();
                return l;
            }
        }

        public static Barrio InsertarBarrio(Int32 idLocalidad, string barrio)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Barrios.Any() ? db.Barrios.Max(b1 => b1.Id) + 1 : 1;
                var b = new Barrio
                {
                    Id = id,
                    IdLocalidad = idLocalidad,
                    Nombre = barrio
                };
                db.Barrios.Add(b);
                db.SaveChanges();
                return b;
            }
        }
    }
}
