using SMPorres.Lib;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class PagosRepository
    {
        internal static void ActualizarCuotas(int idCurso, decimal importe, bool esMatrícula)
        {
            using (var db = new SMPorresEntities())
            {
                var pagos = from p in db.Pagos
                            where p.PlanPago.IdCurso == idCurso &&
                                  !p.Fecha.HasValue
                            select p;
                if (esMatrícula)
                {
                    pagos = pagos.Where(m => m.NroCuota == 0);
                }
                else
                {
                    pagos = pagos.Where(m => m.NroCuota > 0);
                }
                foreach (var m in pagos)
                {
                    m.ImporteCuota = importe;
                }
                db.SaveChanges();
            }
        }

        internal static List<Pago> ObtenerPagos(int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                var q = (from p in db.Pagos
                         join c in db.Cuotas on p.NroCuota equals c.NroCuota into pc
                         from c in pc.DefaultIfEmpty()
                         join mp in db.MediosPago on p.IdMedioPago equals mp.Id into pmp
                         from mp in pmp.DefaultIfEmpty()
                         join ba in db.BecasAlumnos on p.IdBecaAlumno equals ba.Id into pba
                         from ba in pba.DefaultIfEmpty()
                         where p.IdPlanPago == idPlanPago
                         select new
                         {
                             p.Id,
                             p.NroCuota,
                             p.ImporteCuota,
                             p.Fecha,
                             FechaVto = (c == null) ? default(DateTime) : c.VtoCuota,
                             p.ImportePagado,
                             p.IdMedioPago,
                             MedioPago = mp,
                             PorcBeca = (ba == null) ? default(double?) : ba.PorcentajeBeca,
                             p.EsContrasiento,
                             p.Descripcion
                         })
                         .ToList()
                         .Select(
                            p => new Pago
                            {
                                Id = p.Id,
                                NroCuota = p.NroCuota,
                                ImporteCuota = p.ImporteCuota,
                                Fecha = p.Fecha,
                                FechaVto = (p.FechaVto == default(DateTime)) ? new DateTime(ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo, 12, 31) : p.FechaVto,
                                ImportePagado = p.ImportePagado,
                                IdMedioPago = p.IdMedioPago,
                                MedioPago = p.MedioPago,
                                PorcBeca = p.PorcBeca,
                                EsContrasiento = p.EsContrasiento,
                                Descripcion = p.Descripcion
                            }
                        )
                        .OrderBy(p => p.NroCuota);
                return q.ToList();
            }
        }

        internal static Pago ObtenerPago(int idPago)
        {
            using (var db = new SMPorresEntities())
            {
                var p = db.Pagos.Find(idPago);
                if (p != null)
                {
                    db.Entry(p).Reference(p1 => p1.PlanPago).Load();
                    db.Entry(p).Reference(p1 => p1.BecaAlumno).Load();
                    db.Entry(p.PlanPago).Reference(pp => pp.Alumno).Load();
                    db.Entry(p.PlanPago.Alumno).Reference(a => a.TipoDocumento).Load();
                    db.Entry(p.PlanPago).Reference(pp => pp.Curso).Load();
                    db.Entry(p.PlanPago.Curso).Reference(c => c.Carrera).Load();
                }
                return p;
            }
        }

        public static Pago ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
        {
            Pago pago = ObtenerPago(idPago);
            if (pago == null) return null;
            var impBase = pago.ImporteCuota;
            var cc = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
            pago.PorcBeca = 0;
            pago.ImporteBeca = 0;
            pago.PorcDescPagoTermino = 0;
            pago.ImportePagoTermino = 0;
            pago.PorcRecargo = 0;
            pago.ImporteRecargo = 0;
            pago.ImportePagado = pago.ImporteCuota;

            if (pago.NroCuota == 0)
            {
                pago.FechaVto = new DateTime(cc, 12, 31);
                return pago;
            }

            var descBeca = (decimal)0;
            if (pago.BecaAlumno == null)
            {
                descBeca = (decimal)pago.PlanPago.PorcentajeBeca;
            }
            else
            {
                descBeca = (decimal)pago.BecaAlumno.PorcentajeBeca;
            }
            decimal beca = 0;
            if (descBeca > 0)
            {
                beca = Math.Round(impBase * (descBeca / 100));
            }

            var cuota = CuotasRepository.ObtenerCuotas().Where(c => c.NroCuota == pago.NroCuota).FirstOrDefault();
            if (cuota == null)
            {
                return null;
            }
            pago.FechaVto = cuota.VtoCuota;
            var totalAPagar = (decimal)0;
            var impBecado = impBase - beca;
            var conf = ConfiguracionRepository.ObtenerConfiguracion();
            if (fechaCompromiso <= pago.FechaVto)
            {
                var dpt = (decimal)(conf.DescuentoPagoTermino / 100);
                var descPagoTérmino = Math.Round(impBecado * dpt, 2);

                if (fechaCompromiso > pago.FechaVto.Value.AddDays(-conf.DiasVtoPagoTermino ?? 0))
                {
                    dpt = 0;
                    descPagoTérmino = 0;
                }

                totalAPagar = impBase - beca - descPagoTérmino;

                pago.PorcDescPagoTermino = (double)Math.Truncate(dpt * 100);
                pago.ImportePagoTermino = descPagoTérmino;
            }
            else
            {
                var porcRecargo = (conf.InteresPorMora / 100) / 30.0;
                var díasAtraso = Math.Truncate((fechaCompromiso - pago.FechaVto.Value).TotalDays);
                var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                //impBecado = impBase;
                //var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                //totalAPagar = impBase - beca + recargoPorMora;

                beca = 0;
                var recargoPorMora = Math.Round(pago.ImporteCuota * porcRecargoTotal, 2);
                totalAPagar = impBase + recargoPorMora;

                pago.PorcRecargo = porcRecargo;
                pago.ImporteRecargo = recargoPorMora;
            }

            pago.PorcBeca = (double)descBeca;
            pago.ImporteBeca = beca;
            pago.IdBecaAlumno = null;
            pago.ImportePagado = totalAPagar;

            return pago;
        }

        public static bool PagarCuota(Pago pago)
        {
            using (var db = new SMPorresEntities())
            {
                var p = db.Pagos.Find(pago.Id);
                p.Fecha = pago.Fecha;
                p.FechaVto = pago.FechaVto;
                p.PorcBeca = pago.PorcBeca;
                p.ImporteBeca = pago.ImporteBeca;
                p.PorcDescPagoTermino = pago.PorcDescPagoTermino;
                p.ImportePagoTermino = pago.ImportePagoTermino;
                p.PorcRecargo = pago.PorcRecargo;
                p.ImporteRecargo = pago.ImporteRecargo;
                p.ImportePagado = pago.ImportePagado;
                p.IdMedioPago = pago.IdMedioPago;
                p.IdUsuario = Session.CurrentUser.Id;
                p.FechaGrabacion = Configuration.CurrentDate;
                p.Descripcion = pago.Descripcion;
                p.IdArchivo = pago.IdArchivo;
                db.SaveChanges();
                if (p.NroCuota == Configuration.MaxCuotas)
                {
                    p.PlanPago.Estado = (short) EstadoPlanPago.Cancelado;
                }
            }
            return true;
        }

        public static void PagarCuota(SMPorresEntities db, Pago pago)
        {
            var p = db.Pagos.Find(pago.Id);
            p.Fecha = pago.Fecha;
            p.FechaVto = pago.FechaVto;
            p.PorcBeca = pago.PorcBeca;
            p.ImporteBeca = pago.ImporteBeca;
            p.PorcDescPagoTermino = pago.PorcDescPagoTermino;
            p.ImportePagoTermino = pago.ImportePagoTermino;
            p.PorcRecargo = pago.PorcRecargo;
            p.ImporteRecargo = pago.ImporteRecargo;
            p.ImportePagado = pago.ImportePagado;
            p.IdMedioPago = pago.IdMedioPago;
            p.IdUsuario = Session.CurrentUser.Id;
            p.FechaGrabacion = Configuration.CurrentDate;
            p.Descripcion = pago.Descripcion;
            p.IdArchivo = pago.IdArchivo;
            if (p.NroCuota == Configuration.MaxCuotas)
            {
                p.PlanPago.Estado = (short)EstadoPlanPago.Cancelado;
            }
        }
    }
}
