using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class LocalidadesRepository
    {
        public static IEnumerable<Localidad> ObtenerLocalidadesPorDepartamento(int idDepartamento)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var deptos = db.Localidades.Where(l => l.IdDepartamento == idDepartamento).ToList()
                                .Select(
                                    l => new Localidad {
                                                 Id = l.Id,
                                                 Nombre = l.Nombre
                                            }
                                 );
                return deptos.OrderBy(b => b.Nombre).ToList();
            }
        }

        public static Localidad Insertar(int idDepartamento, string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.Localidades.Any(l => l.Nombre.ToLower() == nombre.ToLower() &&
                        l.IdDepartamento == idDepartamento))
                {
                    throw new Exception("Ya existe una localidad con este nombre en este departamento.");
                }

                var id = db.Localidades.Any() ? db.Localidades.Max(l => l.Id) + 1 : 1;
                var loc = new Localidad
                {
                    Id = id,
                    IdDepartamento = idDepartamento,
                    Nombre = nombre
                };
                db.Localidades.Add(loc);
                db.SaveChanges();
                return loc;
            }
        }

        internal static Localidad ObtenerLocalidadPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Localidades.FirstOrDefault(l => l.Id == id);
            }
        }

        internal static void Actualizar(int id, string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Localidades.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la localidad con Id " + id);
                }
                var d = db.Localidades.Find(id);
                d.Nombre = nombre;
                db.SaveChanges();
            }
        }

        internal static void Eliminar(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Localidades.Any(t => t.Id == id))
                {
                    throw new Exception("No existe la localidad con Id " + id);
                }
                var l = db.Localidades.Find(id);
                if (l.Barrios.Any())
                {
                    throw new Exception(String.Format("No se puede eliminar la localidad " +
                        "porque tiene {0} barrios relacionados.", l.Barrios.Count));
                }
                if (l.Domicilios.Any())
                {
                    throw new Exception("No se puede eliminar la localidad porque está relacionada a alumnos.");
                }
                db.Localidades.Remove(l);
                db.SaveChanges();
            }
        }
    }

}
