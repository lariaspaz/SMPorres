using EnvioEMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvioEMail.Repositories
{
    class AlumnosMorososRepository
    {
        public static IEnumerable<AlumnoMoroso> ObtenerAlumnosMorosos(int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from pp in db.PlanesPago
                             join p in db.Pagos on pp.Id equals p.IdPlanPago
                             where pp.Estado == 1 && //Planes de pago activos
                                 p.ImportePagado == null &&// Cuota impaga
                                 p.NroCuota <= CuotasRepository.MáximaCuotaVencida &&
                                 pp.Cursos.Carreras.Id == idCarrera
                             orderby pp.Id
                             select new AlumnoMoroso
                             {
                                 IdPlanPago = pp.Id,
                                 Carrera = pp.Cursos.Carreras.Nombre,
                                 Curso = pp.Cursos.Nombre,
                                 Nombre = pp.Alumnos.Nombre,
                                 Apellido = pp.Alumnos.Apellido,
                                 Documento = pp.Alumnos.NroDocumento.ToString(),
                                 EMail = pp.Alumnos.EMail, //a.EMail,
                                 IdPago = p.Id,
                                 NroCuota = p.NroCuota,
                                 ImporteDeuda = 0,
                                 //ImporteDeuda = PagosRepository.ObtenerDetallePago(p.Id, DateTime.Today).ImportePagado,
                                 CuotasAdeudadas = 1
                             }).ToList();
                //return query.OrderBy(s => s.IdPlanPago).ToList();

                foreach (var item in query)
                {
                    item.ImporteDeuda = PagosRepository.ObtenerDetallePago(item.IdPago, DateTime.Today).ImportePagado;
                }

                var morosos = (from t in query
                               group t by new { t.IdPlanPago, t.Carrera, t.Curso, t.Apellido, t.Nombre, t.Documento, t.EMail }
                               into g
                               select new AlumnoMoroso
                               {
                                   IdPlanPago = g.Key.IdPlanPago,
                                   Carrera = g.Key.Carrera,
                                   Curso = g.Key.Curso,
                                   Apellido = g.Key.Apellido,
                                   Nombre = g.Key.Nombre,
                                   Documento = g.Key.Documento,
                                   EMail = g.Key.EMail,
                                   ImporteDeuda = g.Sum(s => s.ImporteDeuda),
                                   CuotasAdeudadas = g.Count()
                               });
                return morosos;

            }
        }

        internal static IList<Pagos> ObtenerCuotasAdeudadas(int idPlanPago, Int16 cuotaReferencia)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Pagos.Where(x => x.IdPlanPago == idPlanPago && x.ImportePagado == null && x.NroCuota <= cuotaReferencia).ToList();
            }
        }
    }
}
