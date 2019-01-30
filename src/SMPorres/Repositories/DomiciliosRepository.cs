using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMPorres.Models;

namespace SMPorres.Repositories
{
    internal class DomiciliosRepository
    {
        internal static Domicilio ObtenerDomicilio(int idProvincia, int idDepartamento,
            int idLocalidad, int idBarrio)
        {
            Domicilio dom;
            using (var db = new SMPorresEntities())
            {
                dom = (from d in db.Domicilios
                       where d.IdProvincia == idProvincia &
                             d.IdDepartamento == idDepartamento &
                             d.IdLocalidad == idLocalidad &
                             d.IdBarrio == idBarrio
                       select d).FirstOrDefault();

                if (dom == null)
                {
                    dom = new Domicilio();
                    dom.IdProvincia = idProvincia;
                    dom.IdDepartamento = idDepartamento;
                    dom.IdLocalidad = idLocalidad;
                    dom.IdBarrio = idBarrio;
                }
            }
            return dom;
        }

        internal static int ObtenerIdDomicilio(SMPorresEntities db, Domicilio domicilio)
        {
            var dom = (from d in db.Domicilios
                       where d.IdProvincia == domicilio.IdProvincia &
                             d.IdDepartamento == domicilio.IdDepartamento &
                             d.IdLocalidad == domicilio.IdLocalidad &
                             d.IdBarrio == domicilio.IdBarrio
                       select d).FirstOrDefault();
            if (dom == null)
            {
                dom = domicilio;
                dom.Id = db.Domicilios.Any() ? db.Domicilios.Max(d => d.Id) + 1 : 1;
                db.Domicilios.Add(dom);
            }
            return dom.Id;
        }

        internal static Domicilio ObtenerDomicilioPorId(int idDomicilio)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Domicilios.Find(idDomicilio);
            }
        }

        internal static string ObtenerProvincia(int idProvincia)
        {
            using (var db = new SMPorresEntities())
            {
                return (from p in db.Provincias where p.Id == idProvincia select p.Nombre).FirstOrDefault();
            }
        }

        internal static string ObtenerDepartamento(int idDepartamento)
        {
            using (var db = new SMPorresEntities())
            {
                return (from d in db.Departamentos where d.Id == idDepartamento select d.Nombre).FirstOrDefault();
            }
        }

        internal static string ObtenerLocalidad(int idLocalidad)
        {
            using (var db = new SMPorresEntities())
            {
                return (from l in db.Localidades where l.Id == idLocalidad select l.Nombre).FirstOrDefault();
            }
        }

        internal static string ObtenerBarrio(int idBarrio)
        {
            using (var db = new SMPorresEntities())
            {
                return (from b in db.Barrios where b.Id == idBarrio select b.Nombre).FirstOrDefault();
            }
        }

        public static int Insertar(int idProvincia, int idDepartamento, int idLocalidad, int idBarrio)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Domicilios.Any() ? db.Domicilios.Max(d => d.Id) + 1 : 1;
                var dom = new Domicilio
                {
                    Id = id,
                    IdProvincia = idProvincia,
                    IdDepartamento = idDepartamento,
                    IdLocalidad = idLocalidad,
                    IdBarrio = idBarrio
                };
                db.Domicilios.Add(dom);
                db.SaveChanges();
                return dom.Id;
            }
        }
    }
}
