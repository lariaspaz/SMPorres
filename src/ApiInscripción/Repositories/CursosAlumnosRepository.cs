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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static void Insertar(int idCurso, int idAlumno)
        {
            _log.Debug("Insertando alumno en curso");
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
