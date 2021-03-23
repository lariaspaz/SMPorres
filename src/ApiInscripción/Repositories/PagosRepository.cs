using ApiInscripción.Lib;
using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    internal class PagosRepository
    {
        private SMPorresEntities _db;

        internal PagosRepository(SMPorresEntities db)
        {
            _db = db;
        }

        internal Pago ObtenerPago(int idPago)
        {
            var p = _db.Pagos.Find(idPago);
            if (p != null)
            {
                _db.Entry(p).Reference(p1 => p1.PlanPago).Load();
                _db.Entry(p).Reference(p1 => p1.BecaAlumno).Load();
                _db.Entry(p.PlanPago).Reference(pp => pp.Alumno).Load();
                _db.Entry(p.PlanPago.Alumno).Reference(a => a.TipoDocumento).Load();
                _db.Entry(p.PlanPago).Reference(pp => pp.Curso).Load();
                _db.Entry(p.PlanPago.Curso).Reference(c => c.Carrera).Load();
            }
            return p;
        }

       
        public Pago ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
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
                var curso = CursosRepository.ObtenerCursoPorId(pago.PlanPago.Curso.Id);
                if (fechaCompromiso <= curso.FechaVencDescuento)
                {
                    decimal descuentoMatrícula = Convert.ToDecimal(curso.DescuentoMatricula);
                    pago.ImportePagoTermino = descuentoMatrícula;
                    pago.ImportePagado = impBase - descuentoMatrícula;
                }
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

                decimal recargoPorMora = 0;
                if (pago.PlanPago.TipoBeca == (byte)TipoBeca.AplicaSiempre)
                {
                    recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                }
                else
                {
                    recargoPorMora = Math.Round(pago.ImporteCuota * porcRecargoTotal, 2);
                    beca = 0;
                }
                totalAPagar = impBase - beca + recargoPorMora;

                pago.PorcRecargo = porcRecargo;
                pago.ImporteRecargo = recargoPorMora;
            }

            pago.PorcBeca = (double)descBeca;
            pago.ImporteBeca = beca;
            pago.IdBecaAlumno = null;
            pago.ImportePagado = totalAPagar;

            return pago;
        }

        internal static void InsertarMatricula(SMPorresEntities db, Curso curso, int idPlanPago)
        {
            //carga cuota matricula
            var pm = new Pago();
            pm.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
            pm.IdPlanPago = idPlanPago;
            pm.NroCuota = 0;
            pm.ImporteCuota = curso.ImporteMatricula;
            pm.Estado = (byte)EstadoPago.Impago;
            pm.FechaVto = DateTime.Now.AddDays(1).Date;
            db.Pagos.Add(pm);
            db.SaveChanges();
        }

        public static void InsertarPagosCuotas(SMPorresEntities db, Curso curso, int id)
        {
            short minC = CursosRepository.ObtieneMinCuota(curso.Modalidad);
            short maxC = CursosRepository.ObtieneMaxCuota(curso.Modalidad);

            var cuotas = from c in CuotasRepository.ObtenerCuotasActuales()
                         select new { c.NroCuota, c.VtoCuota };

            if (minC != maxC)
            {
                //for (short i = 0; i <= Configuration.MaxCuotas; i++)
                for (short i = minC; i <= maxC; i++)
                {
                    var p = new Pago();
                    p.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
                    p.IdPlanPago = id;
                    p.NroCuota = i;
                    p.ImporteCuota = (i == 0) ? curso.ImporteMatricula : curso.ImporteCuota;
                    p.Estado = (byte)EstadoPago.Impago;
                    p.FechaVto = cuotas.First(c => c.NroCuota == i).VtoCuota;
                    db.Pagos.Add(p);
                    db.SaveChanges();
                }
            }
        }
    }
}
