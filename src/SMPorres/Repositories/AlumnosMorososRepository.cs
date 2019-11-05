using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class AlumnosMorososRepository
    {
        public static IList<AlumnoMoroso> ObtenerAlumnosMorosos(int cuotaMax, int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from pp in db.PlanesPago
                             //join a in db.Alumnos on pp.IdAlumno equals a.Id
                             join p in db.Pagos on pp.Id equals p.IdPlanPago
                             //join c in db.Cursos on pp.IdCurso equals c.Id
                             where pp.Estado == 1 && //Planes de pago activos
                                 p.ImportePagado == null &&// Cuota pagada
                                 p.NroCuota <= cuotaMax
                                 //c.VtoCuota <= DateTime.Today   // Cuotas con vencimiento menor a fecha actual
                                 && pp.Curso.Carrera.Id == idCarrera
                                 //&& pp.Id < 100
                             select new AlumnoMoroso
                             {
                                 IdPlanPago = pp.Id,
                                 Carrera = pp.Curso.Carrera.Nombre,
                                 Curso = pp.Curso.Nombre,
                                 Nombre = pp.Alumno.Nombre, //a.Nombre,
                                 Apellido = pp.Alumno.Apellido, //a.Apellido,
                                 Documento = pp.Alumno.NroDocumento.ToString(), //a.NroDocumento.ToString(),
                                 EMail = pp.Alumno.EMail, //a.EMail,
                                 IdPago = p.Id,
                                 NroCuota = p.NroCuota,
                                 ImporteDeuda = 0,
                                 //ImporteDeuda = PagosRepository.ObtenerDetallePago(p.Id, DateTime.Today).ImportePagado,
                                CuotasAdeudadas = 1
                             });
                return query.OrderBy(s => s.IdPlanPago).ToList();
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
