using ApiInscripción.ConsultasWeb;
using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    public class WebRepository
    {
        public ConsultasWeb.Alumno ObtenerAlumno(SMPorresEntities db, int idAlumno)
        {
            var alumno = db.Alumnos.Find(idAlumno);
            db.Entry(alumno).Reference(t => t.TipoDocumento).Load();
            db.Entry(alumno).Collection(t => t.CursosAlumnos).Load();
            var a = new ConsultasWeb.Alumno
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                TipoDocumento = alumno.TipoDocumento.Descripcion,
                NroDocumento = alumno.NroDocumento,
                Contraseña = alumno.Contraseña
            };

            var conf = ConfiguracionRepository.ObtenerConfiguracion();
            var cicloLectivo = conf.CicloLectivo;
            var díasVtoPagoTermino = conf.DiasVtoPagoTermino ?? 0;
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
                                            Pagos = ObtenerPagos(db, a.Id, ca.IdCurso, díasVtoPagoTermino)
                                        }).ToArray();
            return a;
        }

        private ConsultasWeb.Pago[] ObtenerPagos(Models.SMPorresEntities db, int idAlumno, int idCurso, int díasVtoPagoTermino)
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
                             p.ImportePagado,
                             pp.TipoBeca
                         })
                         .ToList()
                         .Select(
                            p => new ConsultasWeb.Pago
                            {
                                Id = p.Id,
                                IdPlanPago = p.IdPlanPago,
                                NroCuota = p.NroCuota,
                                FechaVto = (p.Cuota == null) ? new DateTime(p.CursoAlumno.CicloLectivo, 12, 31) : p.Cuota.VtoCuota,
                                Fecha = p.Fecha ?? default(DateTime),
                                ImporteCuota = p.ImporteCuota,
                                ImporteBeca = p.ImporteBeca ?? 0,
                                ImporteRecargo = p.ImporteRecargo ?? 0,
                                ImportePagado = p.ImportePagado ?? 0,
                                TipoBeca = p.TipoBeca
                            })
                         .ToArray();

            foreach (var p in query)
            {
                if (p.Fecha == default(DateTime))
                {
                    var pago = PagosRepository.ObtenerDetallePago(p.Id, p.FechaVto.AddDays(-díasVtoPagoTermino));
                    p.ImportePagoTérmino = pago.ImportePagoTermino;
                    p.PorcentajeBeca = (short)Math.Round(pago.PorcBeca ?? 0 * 100);
                    p.ImporteBeca = pago.ImporteBeca;
                }
                p.FechaVtoPagoTérmino = p.FechaVto.AddDays(-díasVtoPagoTermino);
            }

            return query;
        }
    }
}
