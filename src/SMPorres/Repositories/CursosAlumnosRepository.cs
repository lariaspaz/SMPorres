using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class CursosAlumnosRepository
    {
        internal static IList<Alumno> ObtenerAlumnosPorCursoId(int idCurso)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from a in db.Alumnos
                             join ca in db.CursosAlumnos on a.Id equals ca.IdAlumno
                             where a.Estado == 1 && ca.IdCurso == idCurso
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
                                        Estado = a.Estado
                                    });
                return query.OrderBy(a => a.Apellido).ToList();
            }
        }

        internal static List<Curso> ObtenerCursosPorAlumno(int idAlumno)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from ca in db.CursosAlumnos
                             join c in db.Cursos on ca.IdCurso equals c.Id
                             where ca.IdAlumno == idAlumno
                             select c)
                            .ToList()
                            .Select(
                                c => new Curso
                                {
                                    Id = c.Id,
                                    Nombre = c.Nombre,
                                    Carrera = c.Carrera
                                }
                            );
                return query.OrderBy(c => c.Nombre).ToList();
            }
        }

        internal static void Insertar(int idCurso, int idAlumno)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.CursosAlumnos.Any() ? db.CursosAlumnos.Max(c1 => c1.Id) + 1 : 1;
                var ca = new CursosAlumno {
                    Id = id,
                    IdCurso = idCurso,
                    IdAlumno = idAlumno,
                    CicloLectivo = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo
                };
                db.CursosAlumnos.Add(ca);
                db.SaveChanges();
            }
        }

        internal static void Eliminar(int idCurso, int idAlumno)
        {
            using (var db = new SMPorresEntities())
            {
                var ca = db.CursosAlumnos.FirstOrDefault(t => t.IdCurso == idCurso && t.IdAlumno == idAlumno);
                if (ca == null) return;
                db.CursosAlumnos.Remove(ca);
                db.SaveChanges();
            }
        }
    }
}
