using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    static class DepartamentosRepository
    {
        public static IEnumerable<Departamento> ObtenerDepartamentosPorProvincia(int idProvincia)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var deptos = db.Departamentos.Where(d => d.IdProvincia == idProvincia).ToList()
                                .Select(
                                    d => new Departamento {
                                        Id = d.Id,
                                        Nombre = d.Nombre
                                    }
                                 ).ToList();
                return deptos.OrderBy(b => b.Nombre).ToList();
            }
        }

        public static Departamento Insertar(int idProvincia, string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.Departamentos.Any(d => d.Nombre.ToLower() == nombre.ToLower() && 
                        d.IdProvincia == idProvincia))
                {
                    throw new Exception("Ya existe un departamento con este nombre en esta provincia.");
                }

                var id = db.Departamentos.Any() ? db.Departamentos.Max(d => d.Id) + 1 : 1;
                var depto = new Departamento
                {
                    Id = id,
                    IdProvincia = idProvincia,
                    Nombre = nombre
                };
                db.Departamentos.Add(depto);
                db.SaveChanges();
                return depto;
            }
        }

        internal static Departamento ObtenerDepartamentosPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Departamentos.FirstOrDefault(c => c.Id == id);
            }
        }

        internal static void Actualizar(int id, string nombre)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Departamentos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el departamento con Id {0}" + id);
                }
                var d = db.Departamentos.Find(id);
                d.Nombre = nombre;
                db.SaveChanges();
            }

        }

        internal static void Eliminar(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Departamentos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el departamento con Id " + id);
                }
                var d = db.Departamentos.Find(id);
                if (d.Localidades.Any())
                {
                    throw new Exception(String.Format("No se puede eliminar el departamento " + 
                        "porque tiene {0} localidades relacionadas.", d.Localidades.Count));
                }
                db.Departamentos.Remove(d);
                db.SaveChanges();
            }
        }
    }
}
