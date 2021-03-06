﻿using SMPorres.ConsultasWeb;
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
        private static short _cicloLectivo = 0;
        private static short _díasVtoPagoTermino = 0;

        public IEnumerable<Alumno> ObtenerDatos()
        {
            var conf = ConfiguracionRepository.ObtenerConfiguracion();
            _cicloLectivo = conf.CicloLectivo;
            _díasVtoPagoTermino = conf.DiasVtoPagoTermino ?? 0;
            using (var db = new Models.SMPorresEntities())
            {
                var result = (from a in db.Alumnos
                              join ca in db.CursosAlumnos on a.Id equals ca.IdAlumno
                              where
                                ca.CicloLectivo == _cicloLectivo &&
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
                return result;
            }
        }

        public CursoAlumno[] ObtenerCursosAlumnos(Alumno a)
        {
            using (var db = new Models.SMPorresEntities())
            {
                _log.Debug($"Procesando alumno {a.Id} - ciclo lectivo {_cicloLectivo}");
                return (from ca in db.CursosAlumnos
                        where ca.IdAlumno == a.Id &&
                                 ca.CicloLectivo == _cicloLectivo
                        select ca)
                        .ToList()
                        .Select(ca => new CursoAlumno
                        {
                            Id = ca.Id,
                            IdCurso = ca.IdCurso,
                            Curso = ca.Curso.Nombre,
                            IdCarrera = ca.Curso.Carrera.Id,
                            Carrera = ca.Curso.Carrera.Nombre,
                            Pagos = ObtenerPagos(db, a.Id, ca.IdCurso, _díasVtoPagoTermino)
                        })
                        .ToArray();
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
                p.FechaVtoPagoTérmino = p.FechaVto.AddDays(-díasVtoPagoTermino);
                if (p.Fecha == default(DateTime))
                {
                    var pago = PagosRepository.ObtenerDetallePago(p.Id, p.FechaVtoPagoTérmino);
                    p.ImportePagoTérmino = pago.ImportePagoTermino;
                    p.PorcentajeBeca = (short)Math.Round(pago.PorcBeca ?? 0 * 100);
                    p.ImporteBeca = pago.ImporteBeca;
                }

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

        public TasaMora[] ObtenerTasasMora()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var tasas = db.TasasMora.Where(t => t.Estado == (short)Models.EstadoTasaMora.Activa).ToList();
                return (from t in tasas
                       select new TasaMora
                       {
                           Desde = t.Desde,
                           Hasta = t.Hasta,
                           Tasa = t.Tasa,
                           Estado = t.Estado
                       }).ToArray();
            }
        }
    }
}
