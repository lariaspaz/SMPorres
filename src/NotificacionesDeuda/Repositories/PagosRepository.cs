using NotificacionesDeuda.Models;
using System;
using System.Linq;

namespace NotificacionesDeuda.Repositories
{
    class PagosRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Pago ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
        {
            _log.Debug($"Obteniendo detalle de pago - idpago = {idPago}|fechaCompromiso = {fechaCompromiso}");
            Pago pago = ObtenerPago(idPago);
            if (pago == null) return null;
            var impBase = pago.ImporteCuota;
            pago.PorcBeca = 0;
            pago.ImporteBeca = 0;
            pago.PorcDescPagoTermino = 0;
            pago.ImportePagoTermino = 0;
            pago.PorcRecargo = 0;
            pago.ImporteRecargo = 0;
            pago.ImportePagado = pago.ImporteCuota;

            if (pago.NroCuota == 0)
            {
                #region La fila del pago debe tener una fecha de vto
                //var cc = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
                //pago.FechaVto = new DateTime(cc, 12, 31);
                #endregion
                int cantCuotas = CantidadCuotasImpagasMatrícula(pago.IdPlanPago);
                //var curso = CursosRepository.ObtenerCursoPorId(pago.PlanPago.Curso.Id);
                var curso = pago.PlanPago.Curso;
                if (fechaCompromiso <= curso.FechaVencDescuento && cantCuotas == 1) //EsMatriculaSinCuotas(pago))
                {
                    pago.ImportePagoTermino = curso.DescuentoMatricula;
                    pago.ImportePagado = impBase - curso.DescuentoMatricula;
                }
                return pago;
            }

            decimal descBeca = 0;
            if (pago.BecaAlumno == null)
                descBeca = pago.PlanPago.PorcentajeBeca;
            else
                descBeca = (decimal)pago.BecaAlumno.PorcentajeBeca;

            decimal beca = 0;
            if (descBeca > 0)
            {
                beca = Math.Round(impBase * (descBeca / 100));
            }


            #region La fila del pago debe tener una fecha de vto
            //var cl = CursosAlumnosRepository.ObtenerCursosPorAlumno(pago.PlanPago.IdAlumno)
            //            .First(ca => ca.IdCurso == pago.PlanPago.IdCurso)
            //            .CicloLectivo;
            //var cuota = CuotasRepository.ObtenerCuotas()
            //                .FirstOrDefault(c => c.CicloLectivo == cl &&
            //                                     c.NroCuota == pago.NroCuota);
            //if (cuota == null)
            //{
            //    return null;
            //}
            //pago.FechaVto = cuota.VtoCuota;
            #endregion

            decimal totalAPagar = 0;
            var impBecado = impBase - beca;
            var conf = ConfiguracionRepository.ObtenerConfiguracion();
            if (fechaCompromiso <= pago.FechaVto)
            {
                var dpt = (decimal)(conf.DescuentoPagoTermino / 100);
                var descPagoTérmino = Math.Round(impBecado * dpt, 2);

                //Los becados no tienen descuento por pago a término
                if (fechaCompromiso > pago.FechaVto.Value.AddDays(-conf.DiasVtoPagoTermino ?? 0) || beca > 0)
                {
                    dpt = 0;
                    descPagoTérmino = 0;
                }

                //totalAPagar = impBase - beca - descPagoTérmino;
                totalAPagar = impBecado - descPagoTérmino;

                //pago.PorcDescPagoTermino = (double)Math.Truncate(dpt * 100);
                pago.PorcDescPagoTermino = conf.DescuentoPagoTermino;
                pago.ImportePagoTermino = descPagoTérmino;
            }
            else
            {
                decimal recargoPorMora = 0;
                double porcRecargo = 0;

                #region Cálculo con tasa única, sin tipo de beca (no se usa)
                //impBecado = impBase;
                //var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                //totalAPagar = impBase - beca + recargoPorMora;
                #endregion

                #region Cálculo con tasa única y tipo de beca (no se usa)
                //var porcRecargo = (conf.InteresPorMora / 100) / 30.0;
                //var díasAtraso = Math.Truncate((fechaCompromiso - pago.FechaVto.Value).TotalDays);
                //var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                //if (pago.PlanPago.TipoBeca == (byte)TipoBeca.AplicaSiempre)
                //{
                //    recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
                //}
                //else
                //{
                //    recargoPorMora = Math.Round(pago.ImporteCuota * porcRecargoTotal, 2);
                //    beca = 0;
                //}
                #endregion

                if (pago.PlanPago.TipoBeca == (byte)TipoBeca.AplicaHastaVto)
                {
                    beca = 0;
                }
                recargoPorMora = CalcularMoraPorTramo(fechaCompromiso, pago, impBase, impBecado);
                totalAPagar = impBase - beca + recargoPorMora;

                pago.PorcRecargo = porcRecargo;
                pago.ImporteRecargo = recargoPorMora;
            }

            pago.PorcBeca = (double)descBeca;
            pago.ImporteBeca = beca;
            //pago.IdBecaAlumno = null;
            pago.ImportePagado = totalAPagar;

            return pago;
        }

        #region Cálculo de mora con tasas por tramo
        private static decimal CalcularMoraPorTramo(DateTime fechaCompromiso, Pago pago, decimal impBase,
            decimal impBecado)
        {
            if (pago.FechaVto.HasValue)
            {
                var vto = pago.FechaVto.Value;
                if (vto.Month == DateTime.Now.Month && vto.Year == DateTime.Now.Year)
                {
                    return 0;
                }
            }

            decimal recargoPorMora;
            if (TasasMoraRepository.ValidarTasas() == TasasMoraRepository.ValidarTasasResult.Ok)
            {
                var tasas = from t in TasasMoraRepository.ObtenerTasas()
                            where t.Estado == (short)EstadoTasaMora.Activa
                            select new TasaMora
                            {
                                Tasa = (t.Tasa / 100) / 30,
                                Desde = t.Desde < pago.FechaVto.Value ? pago.FechaVto.Value : t.Desde,
                                Hasta = t.Hasta > fechaCompromiso ? fechaCompromiso : t.Hasta.AddDays(1)
                            };
                tasas = tasas.Where(t => t.Desde <= t.Hasta);
                _log.Debug(String.Join("\n", tasas.Select(t => new { TasaDiaria = t.Tasa, t.Desde, t.Hasta })));
                _log.Debug("Tipo de beca: " + (TipoBeca)pago.PlanPago.TipoBeca);

                if (pago.PlanPago.TipoBeca == (byte)TipoBeca.AplicaSiempre)
                {
                    _log.Debug("Importe becado: " + impBecado);
                    _log.Debug(String.Join("\n", tasas.Select(t => new
                    {
                        TotalDays = (t.Hasta - t.Desde).TotalDays,
                        Tasa = t.Tasa,
                        DaysXTasa = (t.Hasta - t.Desde).TotalDays * t.Tasa,
                        Total = impBecado * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa)
                    })));

                    recargoPorMora = tasas.Sum(
                            t => Math.Round(impBecado * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa), 2)
                        );
                }
                else
                {
                    _log.Debug("Importe base: " + impBase);
                    _log.Debug(String.Join("\n", tasas.Select(t => new
                    {
                        TotalDays = (t.Hasta - t.Desde).TotalDays,
                        Tasa = t.Tasa,
                        DaysXTasa = (t.Hasta - t.Desde).TotalDays * t.Tasa,
                        Total = impBase * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa)
                    })));

                    recargoPorMora = tasas.Sum(
                            t => Math.Round(impBase * (decimal)((t.Hasta - t.Desde).TotalDays * t.Tasa), 2)
                        );
                }

            }
            else
            {
                recargoPorMora = 0;
            }
            return recargoPorMora;
        }
        #endregion

        private static int CantidadCuotasImpagasMatrícula(decimal idPlanPago)
        {
            int cc = 0;
            using (var db = new SMPorresEntities())
            {
                cc = db.Pagos.Where(x => x.IdPlanPago == idPlanPago && x.NroCuota == 0
                                    && x.Estado != (short)EstadoPago.Baja
                                    && x.Estado != (short)EstadoPago.Pagado)
                    .Count();
            }
            return cc;
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
                    db.Entry(p.PlanPago.Alumno).Reference(a => a.TiposDocumento).Load();
                    db.Entry(p.PlanPago).Reference(pp => pp.Curso).Load();
                    db.Entry(p.PlanPago.Curso).Reference(c => c.Carreras).Load();
                }
                return p;
            }
        }
    }
}
