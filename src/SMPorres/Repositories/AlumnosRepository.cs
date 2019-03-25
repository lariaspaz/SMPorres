using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                var a = db.Alumnos.Find(id);
                if (a != null)
                {
                    db.Entry(a).Reference(t => t.TipoDocumento).Load();
                    db.Entry(a).Reference(d => d.Domicilio).Load();
                    if (a.Domicilio != null)
                    {
                        db.Entry(a.Domicilio).Reference(p => p.Provincia).Load();
                        db.Entry(a.Domicilio).Reference(d => d.Departamento).Load();
                        db.Entry(a.Domicilio).Reference(l => l.Localidad).Load();
                        db.Entry(a.Domicilio).Reference(b => b.Barrio).Load();
                    }
                }
                return a;
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
                var alumno = db.Alumnos.Find(id);
                if (alumno.CursosAlumnos.Any())
                {
                    var s = " - " + String.Join("\n - ", alumno.CursosAlumnos.Select(ca => String.Format("{0} de {1}", ca.Curso.Nombre, ca.Curso.Carrera.Nombre)));
                    throw new Exception(String.Format("El alumno está asignado a los cursos:\n{1}", alumno.CursosAlumnos.Count(), s));
                }
                if (alumno.PlanesPago.Any())
                {
                    var s = " - " + String.Join("\n - ", alumno.PlanesPago.Select(pp => pp.Curso.Nombre));
                    throw new Exception(String.Format("El alumno tiene {0} planes de pago en los cursos:\n{1}", alumno.PlanesPago.Count(), s));
                }
                if (alumno.Domicilio != null && alumno.Domicilio.Alumnos.Count == 1)
                {
                    var dom = db.Domicilios.Find(alumno.IdDomicilio);
                    db.Domicilios.Remove(dom);
                }
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

        public static IEnumerable<Models.InformesModels.AlumnoCursante> ObtenerAlumnosPorEstado(int idCarrera, 
            int idCurso, EstadoAlumno estado)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (
                    from ca in db.CursosAlumnos
                    join a in db.Alumnos on ca.IdAlumno equals a.Id
                    where a.Estado == (byte)estado
                    select new Models.InformesModels.AlumnoCursante
                    {
                        IdCarrera = ca.Curso.IdCarrera,
                        Carrera = ca.Curso.Carrera.Nombre,
                        IdCurso = ca.IdCurso,
                        Curso = ca.Curso.Nombre,
                        Nombre = a.Nombre,
                        Apellido = a.Apellido,
                        EMail = a.EMail,
                        Estado = a.Estado,
                        Documento = a.TipoDocumento.Descripcion + " " + a.NroDocumento
                    });

                if (idCarrera > 0)
                {
                    query = query.Where(a => a.IdCarrera == idCarrera);
                }
                if (idCurso > 0)
                {
                    query = query.Where(a => a.IdCurso == idCurso);
                }
                return query.OrderBy(a => a.IdCarrera).ThenBy(a => a.IdCurso).ThenBy(a => a.Estado).ToList();
            }
        }

        public static string GenerarContraseña(int idAlumno, ref string pwdEncriptada)
        {
            using (var db = new SMPorresEntities())
            {
                var a = db.Alumnos.Find(idAlumno);
                var pwd = Lib.Security.Cryptography.GenerarContraseña();
                pwdEncriptada = Lib.Security.Cryptography.CalcularSHA512(pwd);
                a.Contraseña = pwdEncriptada;
                db.SaveChanges();
                return pwd;
            }
        }
    }
}
