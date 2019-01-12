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
    }

}
