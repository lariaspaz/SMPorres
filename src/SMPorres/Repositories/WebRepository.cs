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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<Alumno> ObtenerDatos()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var conf = ConfiguracionRepository.ObtenerConfiguracion();
                var cicloLectivo = conf.CicloLectivo;
                var díasVtoPagoTermino = conf.DiasVtoPagoTermino ?? 0;

                var result = (from a in db.Alumnos
                              join ca in db.CursosAlumnos on a.Id equals ca.IdAlumno
                              where
                                ca.CicloLectivo == cicloLectivo &&
                                a.Contraseña != null
                              select a
                              ).ToList()
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
                foreach (var a in result)
                {
                    _log.Debug($"Procesando alumno {a.Id} - ciclo lectivo {cicloLectivo}");
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
                                        })
                                        .ToArray();
                }
                return result;
            }
        }

        private Pago[] ObtenerPagos(Models.SMPorresEntities db, int idAlumno, int idCurso, int díasVtoPagoTermino)
        {
            var query = (from pp in db.PlanesPago
                         join p in db.Pagos on pp.Id equals p.IdPlanPago
                         join ca in db.CursosAlumnos on
                            new { pp.IdAlumno, pp.IdCurso } equals new { ca.IdAlumno, ca.IdCurso }
                            into ca2
                         join c in db.Cuotas on
                            new { cl = ca2.FirstOrDefault().CicloLectivo, p.NroCuota } equals
                            new { cl = c.CicloLectivo.Value, c.NroCuota }
                            into pc
                         from c in pc.DefaultIfEmpty()
                         where
                            pp.IdAlumno == idAlumno &&
                            pp.IdCurso == idCurso //&& p.Fecha != null
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
                             pp.TipoBeca,
                             p.FechaVto,
                             p.Estado
                         })
                         .ToList()
                         .Select(
                            p => new Pago
                            {
                                Id = p.Id,
                                IdPlanPago = p.IdPlanPago,
                                NroCuota = p.NroCuota,
                                //FechaVto = (p.Cuota == null) ? new DateTime(p.CursoAlumno.CicloLectivo, 12, 31) : p.Cuota.VtoCuota,
                                FechaVto = p.FechaVto ?? p.Cuota.VtoCuota,
                                Fecha = p.Fecha ?? default(DateTime),
                                ImporteCuota = p.ImporteCuota,
                                ImporteBeca = p.ImporteBeca ?? 0,
                                ImporteRecargo = p.ImporteRecargo ?? 0,
                                ImportePagado = p.ImportePagado ?? 0,
                                TipoBeca = p.TipoBeca,
                                Estado = p.Estado
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

                int cc = PagosRepository.CantidadCuotasImpagasMatrícula(p.IdPlanPago);
                var curso = CursosRepository.ObtenerCurso(p.IdPlanPago);
                if (p.NroCuota == 0)
                {
                    if (cc == 1) p.FechaVtoPagoTérmino = (DateTime)curso.FechaVencDescuento;
                    if (cc == 3) p.FechaVtoPagoTérmino = p.FechaVto;
                }

            }

            return query;
        }

        public static IEnumerable<TasaMora> ObtenerTasasMora()
        {
            using (var db = new Models.SMPorresEntities())
            {
                return from t in db.TasasMora.ToList()
                       select new TasaMora
                       {
                           Desde = t.Desde,
                           Hasta = t.Hasta,
                           Tasa = t.Tasa,
                           Estado = t.Estado
                       };
            }
        }
    }
}
