using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class AlumnosRepository
    {
        public static IList<Alumno> ObtenerAlumnos()
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from a in db.Alumnos
                             join td in db.TiposDocumento on a.IdTipoDocumento equals td.Id
                             select a)
                             .ToList()
                                .Select(
                                    a => new Alumno
                                    {
                                        Id = a.Id,
                                        Nombre = a.Nombre,
                                        Apellido = a.Apellido,
                                        IdTipoDocumento = a.IdTipoDocumento,
                                        TipoDocumento = a.TipoDocumento,
                                        NroDocumento = a.NroDocumento,
                                        FechaNacimiento = a.FechaNacimiento,
                                        EMail = a.EMail,
                                        Direccion = a.Direccion,
                                        IdDomicilio = a.IdDomicilio,
                                        Domicilio = a.Domicilio,
                                        Estado = a.Estado,
                                        Sexo = a.Sexo                                                                                
                                    });
                return query.OrderBy(a => a.Apellido).ToList();
            }
        }

        public static IList<Alumno> BuscarAlumnosPorDocumento(string nroDocumento)
        {
            var query = ObtenerAlumnos().Where(a => a.NroDocumento.ToString().Contains(nroDocumento))
                        .Select(
                            a => new Alumno
                            {
                                Id = a.Id,
                                NroDocumento = a.NroDocumento,
                                Nombre = a.Nombre + " " + a.Apellido
                            });
            return query.ToList();
        }

        public static IList<Alumno> BuscarAlumnosPorNombre(string nombre)
        {
            var query = ObtenerAlumnos().Where(a => (a.Nombre + a.Apellido).Contains(nombre))
                        .Select(
                            a => new Alumno
                            {
                                Id = a.Id,
                                NroDocumento = a.NroDocumento,
                                Nombre = a.Nombre + " " + a.Apellido
                            });
            return query.ToList();
        }

        internal static Alumno BuscarAlumnoPorNroDocumento(decimal nroDocumento)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Alumnos.FirstOrDefault(a => a.NroDocumento == nroDocumento);
            }
        }

        internal static Alumno ObtenerAlumnoPorId(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Alumnos.Find(id);
            }
        }

        internal static Domicilio ObtenerDomicilio(Int32? id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Domicilios.Find(id);
            }
        }

        public static void Actualizar(decimal id, string nombre, string apellido, int idTipoDocumento, decimal nroDocumento,
            DateTime fechaNacimiento, string email, string dirección, Domicilio domicilio, byte estado, char sexo)
        {
            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    if (!db.Alumnos.Any(t => t.Id == id))
                    {
                        throw new Exception(String.Format("No existe el alumno {0} - {1}, {2}", id, apellido, nombre));
                    }
                    var a = db.Alumnos.Find(id);
                    a.Nombre = nombre;
                    a.Apellido = apellido;
                    a.IdTipoDocumento = idTipoDocumento;
                    a.NroDocumento = nroDocumento;
                    a.FechaNacimiento = fechaNacimiento;
                    a.EMail = email;
                    a.Direccion = dirección;
                    a.IdDomicilio = DomiciliosRepository.ObtenerIdDomicilio(db, domicilio);
                    a.Estado = estado;
                    if (a.Estado != estado)
                    {
                        a.Estado = estado;
                    }
                    a.Sexo = sexo.ToString();
                    db.SaveChanges();
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        public static void Eliminar(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Alumnos.Any(t => t.Id == id))
                {
                    throw new Exception("No existe el alumno con Id " + id);
                }
                var cursos = from a in db.Alumnos
                            join t in db.CursosAlumnos on a.Id equals t.IdAlumno
                            where a.Id == id
                            select t.Curso.Nombre;
                if (cursos.Any())
                {
                    var s = " - " + String.Join("\n - ", cursos);
                    throw new Exception(String.Format("El alumno está asignado a los cursos:\n{1}", cursos.Count(), s));
                }

                var planes = from a in db.Alumnos
                             join p in db.PlanesPago on a.Id equals p.IdAlumno
                             where a.Id == id
                             select p.Curso.Nombre;
                if (planes.Any())
                {
                    var s = " - " + String.Join("\n - ", planes);
                    throw new Exception(String.Format("El alumno tiene {0} planes de pago en los cursos:\n{1}", planes.Count(), planes));
                }

                var domics = from a in db.Alumnos
                             join d in db.Domicilios on a.IdDomicilio equals d.Id
                             where a.Id == id
                             select d;
                if (domics.Count() == 1)
                {
                    db.Domicilios.Remove(domics.First());
                }
                var alumno = db.Alumnos.Find(id);
                db.Alumnos.Remove(alumno);
                db.SaveChanges();
            }
        }

        public static Alumno Insertar(string nombre, string apellido, int idTipoDocumento, decimal nroDocumento,
            DateTime fechaNacimiento, string email, string dirección, Domicilio domicilio, byte estado, char sexo)
        {
            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    var id = db.Alumnos.Any() ? db.Alumnos.Max(a1 => a1.Id) + 1 : 1;
                    var a = new Alumno
                    {
                        Id = id,
                        Nombre = nombre,
                        Apellido = apellido,
                        IdTipoDocumento = idTipoDocumento,
                        NroDocumento = nroDocumento,
                        FechaNacimiento = fechaNacimiento,
                        EMail = email,
                        Direccion = dirección,
                        IdDomicilio = DomiciliosRepository.ObtenerIdDomicilio(db, domicilio),
                        Estado = estado,
                        Sexo = sexo.ToString()
                    };
                    db.Alumnos.Add(a);
                    db.SaveChanges();
                    trx.Commit();
                    return a;
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }
    }
}
