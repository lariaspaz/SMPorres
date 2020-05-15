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
            var ciclo = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
            using (var db = new SMPorresEntities())
            {
                db.Database.Log = s => System.Diagnostics.Debug.Print(s);
                var query = (from a in db.Alumnos
                             join ca in db.CursosAlumnos on a.Id equals ca.IdAlumno
                             where 
                                a.Estado == 1 && 
                                ca.IdCurso == idCurso &&
                                ca.CicloLectivo == ciclo
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
                                        Estado = a.Estado
                                    });
                return query.OrderBy(a => a.NroDocumento).ToList();
            }
        }

        internal static List<CursosAlumno> ObtenerCursosPorAlumno(int idAlumno)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from ca in db.CursosAlumnos
                             where ca.IdAlumno == idAlumno
                             select new
                             {
                                 ca.Id,
                                 IdCurso = ca.Curso.Id,
                                 ca.CicloLectivo,
                                 Curso = ca.Curso.Nombre,
                                 Carrera = ca.Curso.Carrera.Nombre
                             })
                            .ToList()
                            .Select(
                                c => new CursosAlumno
                                {
                                    Id = c.Id,
                                    IdCurso = c.IdCurso,
                                    CicloLectivo = c.CicloLectivo,
                                    Curso = new Curso { Nombre = c.Curso, Carrera = new Carrera { Nombre = c.Carrera } },
            }
                            );
                return query.OrderBy(c => c.CicloLectivo).ThenBy(c => c.Curso.Nombre).ToList();
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

        public static IEnumerable<short> ObtenerCiclosLectivos()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var query = (from cc in db.CursosAlumnos
                             select cc.CicloLectivo).Distinct();

                return query.ToList();
            }
        }
    }
}
