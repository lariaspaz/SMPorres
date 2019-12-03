using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    class CursosAlumnosRepository
    {
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
    }
}
