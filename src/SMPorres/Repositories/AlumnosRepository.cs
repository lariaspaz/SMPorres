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
                                        NroDocumento = a.NroDocumento,
                                        FechaNacimiento = a.FechaNacimiento,
                                        EMail = a.EMail,
                                        Direccion = a.Direccion,
                                        IdDomicilio = a.IdDomicilio,
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

        internal static Domicilio ObtenerDomicilio(Int32 id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Domicilios.Find(id);
            }
        }

        public static void Actualizar(decimal id, string nombre, string apellido, Int32 idTipoDoc, decimal nroDoc,
            DateTime fechaNac, string email, string dirección, Int32 idDomicilio, byte estado, char sexo)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Alumnos.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe el alumno {0} - {1}, {2}", id, apellido, nombre));
                }
                var a = db.Alumnos.Find(id);
                a.Nombre = nombre;
                a.Apellido = apellido;
                a.IdTipoDocumento = idTipoDoc;
                a.NroDocumento = nroDoc;
                a.FechaNacimiento = fechaNac;
                a.EMail = email;
                a.Direccion = dirección;
                a.IdDomicilio = idDomicilio;
                a.Estado = estado;
                if (a.Estado != estado)
                {
                    a.Estado = estado;
                }
                a.Sexo = sexo.ToString();
                db.SaveChanges();
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
                var a = db.Alumnos.Find(id);
                db.Alumnos.Remove(a);
                db.SaveChanges();
            }
        }

        public static Alumno Insertar(string nombre, string apellido, Int32 idTipoDoc, decimal nroDoc,
            DateTime fechaNac, string email, string dirección, Int32 idDomicilio, byte estado, char sexo)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.Alumnos.Any() ? db.Alumnos.Max(a1 => a1.Id) + 1 : 1;
                var a = new Alumno
                {
                    Id = id,
                    Nombre = nombre,
                    Apellido = apellido,
                    IdTipoDocumento = idTipoDoc,
                    NroDocumento = nroDoc,
                    FechaNacimiento = fechaNac,
                    EMail = email,
                    Direccion = dirección,
                    IdDomicilio = idDomicilio,
                    Estado = estado,
                    Sexo = sexo.ToString()
                };
                db.Alumnos.Add(a);
                db.SaveChanges();
                return a;
            }
        }

        public static bool CursoAsignado(decimal id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.CursosAlumnos.Any(t => t.IdAlumno == id))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


    }
}
