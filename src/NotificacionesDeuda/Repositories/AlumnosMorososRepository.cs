using NotificacionesDeuda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Repositories
{
    class AlumnosMorososRepository
    {
        public static IEnumerable<AlumnoMoroso> ObtenerAlumnosMorosos(int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from pp in db.PlanesPago
                             join p in db.Pagos on pp.Id equals p.IdPlanPago
                             join ca in db.CursosAlumnos on new { pp.IdCurso, pp.IdAlumno } equals new { ca.IdCurso, ca.IdAlumno }  //Control que 
                             where pp.Estado == 1 && //Planes de pago activos
                                 p.ImportePagado == null &&// Cuota impaga
                                 p.FechaVto < DateTime.Now  &&
                                 pp.Curso.Carreras.Id == idCarrera &&
                                 pp.Alumno.EMail != ""
                             orderby pp.Id
                             select new AlumnoMoroso
                             {
                                 IdPlanPago = pp.Id,
                                 Carrera = pp.Curso.Carreras.Nombre,
                                 Curso = pp.Curso.Nombre,
                                 Nombre = pp.Alumno.Nombre,
                                 Apellido = pp.Alumno.Apellido,
                                 NroDocumento = pp.Alumno.NroDocumento,
                                 EMail = pp.Alumno.EMail, //a.EMail,
                                 IdPago = p.Id,
                                 NroCuota = p.NroCuota,
                                 ImporteDeuda = 0,
                                 //ImporteDeuda = PagosRepository.ObtenerDetallePago(p.Id, DateTime.Today).ImportePagado,
                                 CuotasAdeudadas = 1
                             }).ToList();

                foreach (var item in query)
                {
                    item.ImporteDeuda = PagosRepository.ObtenerDetallePago(item.IdPago, DateTime.Today).ImportePagado;
                }

                var morosos = (from t in query
                               group t by new { t.IdPlanPago, t.Carrera, t.Curso, t.Apellido, t.Nombre, t.NroDocumento, t.EMail }
                               into g
                               select new AlumnoMoroso
                               {
                                   IdPlanPago = g.Key.IdPlanPago,
                                   Carrera = g.Key.Carrera,
                                   Curso = g.Key.Curso,
                                   Apellido = g.Key.Apellido,
                                   Nombre = g.Key.Nombre,
                                   NroDocumento = g.Key.NroDocumento,
                                   EMail = g.Key.EMail,
                                   ImporteDeuda = g.Sum(s => s.ImporteDeuda),
                                   CuotasAdeudadas = g.Count()
                               });
                return morosos;

            }
        }

        internal static IList<Pago> ObtenerCuotasAdeudadas(int idPlanPago, Int16 cuotaReferencia)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Pagos.Where(x => x.IdPlanPago == idPlanPago && x.ImportePagado == null && x.NroCuota <= cuotaReferencia).ToList();
            }
        }
    }
}
