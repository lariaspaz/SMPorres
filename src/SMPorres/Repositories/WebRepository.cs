using SMPorres.ConsultasWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    public class WebRepository
    {
        public IEnumerable<Alumno> ObtenerDatos()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var result = (from a in db.Alumnos select a).ToList()
                            .Select(a => new Alumno
                            {
                                Id = a.Id,
                                Nombre = a.Nombre,
                                Apellido = a.Apellido,
                                TipoDocumento = a.TipoDocumento.Descripcion,
                                NroDocumento = a.NroDocumento,
                                Contraseña = a.Contraseña
                            })
                            .ToList();
                var cicloLectivo = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
                foreach (var a in result)
                {
                    a.CursosAlumnos = (from ca in db.CursosAlumnos
                                       where ca.IdAlumno == a.Id &&
                                                ca.CicloLectivo == cicloLectivo
                                       select ca).ToList()
                                        .Select(ca => new CursoAlumno
                                        {
                                            Id = ca.Id,
                                            IdCurso = ca.IdCurso,
                                            Curso = ca.Curso.Nombre,
                                            IdCarrera = ca.Curso.Carrera.Id,
                                            Carrera = ca.Curso.Carrera.Nombre,
                                            Pagos = ObtenerPagos(db, a.Id, ca.IdCurso)
                                        })
                                        .ToArray();
                }
                return result;
            }
        }

        private Pago[] ObtenerPagos(Models.SMPorresEntities db, int idAlumno, int idCurso)
        {
            var query = (from pp in db.PlanesPago
                         join p in db.Pagos on pp.Id equals p.IdPlanPago
                         join c in db.Cuotas on p.NroCuota equals c.NroCuota into pc
                         from c in pc.DefaultIfEmpty()
                         join ca in db.CursosAlumnos on new { pp.IdAlumno, pp.IdCurso } equals new { ca.IdAlumno, ca.IdCurso } into ca2
                         where pp.IdAlumno == idAlumno && pp.IdCurso == idCurso //&& p.Fecha != null
                         select new
                         {
                             p.Id,
                             p.IdPlanPago,
                             p.NroCuota,
                             Cuota = c,
                             CursoAlumno = ca2.FirstOrDefault(),
                             p.Fecha,
                             p.ImporteCuota,
                             p.ImporteBeca,
                             p.ImporteRecargo,
                             p.ImportePagado
                         })
                         .ToList()
                         .Select(
                            p => new Pago
                            {
                                Id = p.Id,
                                IdPlanPago = p.IdPlanPago,
                                NroCuota = p.NroCuota,
                                FechaVto = (p.Cuota == null) ? new DateTime(p.CursoAlumno.CicloLectivo, 12, 31) : p.Cuota.VtoCuota,
                                Fecha = p.Fecha ?? default(DateTime),
                                ImporteCuota = p.ImporteCuota,
                                ImporteBeca = p.ImporteBeca ?? 0,
                                ImporteRecargo = p.ImporteRecargo ?? 0,
                                ImportePagado = p.ImportePagado ?? 0
                            })
                         .ToArray();

            foreach (var p in query)
            {
                if (p.Fecha == default(DateTime))
                {
                    var pago = PagosRepository.ObtenerDetallePago(p.Id, p.FechaVto);
                    p.ImportePagoTérmino = pago.ImportePagoTermino;
                    p.PorcentajeBeca = (short)Math.Round(pago.PorcBeca ?? 0 * 100);
                    p.ImporteBeca = pago.ImporteBeca;
                }
            }

            return query;
        }
    }
}
