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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static void ActualizarCuotas(int idCurso, decimal importe, bool esMatrícula)
        {
            var ciclo = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
            using (var db = new SMPorresEntities())
            {
                var pagos = from p in db.Pagos
                            join pp in db.PlanesPago on p.IdPlanPago equals pp.Id
                            join ca in db.CursosAlumnos on
                                new { pp.IdCurso, pp.IdAlumno } equals new { ca.IdCurso, ca.IdAlumno }
                            where !p.Fecha.HasValue &&
                                    pp.IdCurso == idCurso &&
                                    ca.CicloLectivo == ciclo
                            select p;
                db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
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
                var pagos = from p in db.Pagos
                            join pp in db.PlanesPago on p.IdPlanPago equals pp.Id
                            join ca in db.CursosAlumnos on new { pp.IdCurso, pp.IdAlumno } equals
                                new { ca.IdCurso, ca.IdAlumno }
                            select new
                            {
                                p.Id,
                                p.NroCuota,
                                p.IdMedioPago,
                                p.IdBecaAlumno,
                                p.IdPlanPago,
                                p.Estado,
                                p.ImporteCuota,
                                p.Fecha,
                                p.ImportePagado,
                                p.EsContrasiento,
                                p.Descripcion,
                                p.FechaVto,
                                ca.CicloLectivo
                            };

                var q = (from p in pagos
                         join c in db.Cuotas on new { cl = p.CicloLectivo, c = p.NroCuota } equals
                            new { cl = c.CicloLectivo.Value, c = c.NroCuota } into pc
                         from c in pc.DefaultIfEmpty()
                         join mp in db.MediosPago on p.IdMedioPago equals mp.Id into pmp
                         from mp in pmp.DefaultIfEmpty()
                         join ba in db.BecasAlumnos on p.IdBecaAlumno equals ba.Id into pba
                         from ba in pba.DefaultIfEmpty()
                         where
                            p.IdPlanPago == idPlanPago &&
                            p.Estado != (short)EstadoPago.Baja
                         select new
                         {
                             p.Id,
                             p.NroCuota,
                             p.ImporteCuota,
                             p.Fecha,
                             //FechaVto = (c == null) ? default(DateTime) : c.VtoCuota,
                             p.FechaVto,
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
                                //FechaVto = (p.FechaVto == default(DateTime)) ? new DateTime(ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo, 12, 31) : p.FechaVto,
                                FechaVto = p.FechaVto,
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

        internal static void ActualizarVencimientosCuotasImpagos(SMPorresEntities db, short nroCuota,
            short cicloLectivo, DateTime vtoCuota)
        {
            var ids = from p in db.Pagos
                      join pp in db.PlanesPago on p.IdPlanPago equals pp.Id
                      join ca in db.CursosAlumnos on
                        new { pp.IdCurso, pp.IdAlumno } equals new { ca.IdCurso, ca.IdAlumno }
                      where
                            p.NroCuota == nroCuota &&
                            p.Fecha == null &&
                            p.Estado == (short)EstadoPago.Impago &&
                            pp.Estado == (short)EstadoPlanPago.Vigente &&
                            ca.CicloLectivo == cicloLectivo
                      select p.Id;
            var sql = $"UPDATE Pagos SET FechaVto = @p0 WHERE Id IN ({ String.Join(",", ids.ToList()) })";
            db.Database.ExecuteSqlCommand(sql, vtoCuota);

        }

        internal static List<Pago> CrearCuotas(SMPorresEntities db, PlanPago planPago, int modalidad)
        {
            //leer modalidad y obtener minCuota y maxCuota
            short minC = CursosRepository.ObtieneMinCuota(modalidad);
            short maxC = CursosRepository.ObtieneMaxCuota(modalidad);
            var result = new List<Pago>();
            if (minC != maxC)
            {
                var curso = CursosRepository.ObtenerCursoPorId(planPago.IdCurso);
                var cuotas = from c in CuotasRepository.ObtenerCuotasActuales()
                             select new { c.NroCuota, c.VtoCuota };
                for (short i = minC; i <= maxC; i++)
                {
                    var p = new Pago();
                    p.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
                    p.IdPlanPago = planPago.Id;
                    p.NroCuota = i;
                    p.ImporteCuota = (i == 0) ? curso.ImporteMatricula : curso.ImporteCuota;
                    p.Estado = (byte)EstadoPago.Impago;
                    p.FechaVto = cuotas.First(c => c.NroCuota == i).VtoCuota;
                    db.Pagos.Add(p);
                    db.SaveChanges();
                    result.Add(p);
                }
            }
            return result;
        }

        internal static Pago CrearMatrícula(SMPorresEntities db, PlanPago planPago)
        {
            var p = new Pago();
            p.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
            p.IdPlanPago = planPago.Id;
            p.NroCuota = 0;
            var curso = CursosRepository.ObtenerCursoPorId(planPago.IdCurso);
            p.ImporteCuota = curso.ImporteMatricula;
            var vto = (from c in CuotasRepository.ObtenerCuotasActuales()
                       where c.NroCuota == 0
                       select c.VtoCuota).First();
            p.FechaVto = vto;
            p.Estado = (byte)EstadoPago.Impago;
            db.Pagos.Add(p);
            db.SaveChanges();
            return p;
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

                recargoPorMora = CalcularMoraPorTramo(fechaCompromiso, pago, impBase, impBecado, ref beca);
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
            decimal impBecado, ref decimal beca)
        {
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
                    beca = 0;
                }

            }
            else
            {
                recargoPorMora = 0;
            }
            return recargoPorMora;
        }
        #endregion

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
                p.Estado = (byte)EstadoPago.Pagado;
                db.SaveChanges();
                if (p.NroCuota == Configuration.MaxCuotas)
                {
                    p.PlanPago.Estado = (short)EstadoPlanPago.Cancelado;
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
            p.Estado = (byte)EstadoPago.Pagado;
            if (p.NroCuota == Configuration.MaxCuotas)
            {
                p.PlanPago.Estado = (short)EstadoPlanPago.Cancelado;
            }
        }

        public static void EliminarCuotaGenerada(int nroCuota, int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Pagos.Any(t => t.IdPlanPago == idPlanPago & t.NroCuota == nroCuota))
                {
                    throw new Exception("No existe la cuota generada " + nroCuota);
                }
                var cGen = db.Pagos.FirstOrDefault(x =>
                        x.IdPlanPago == idPlanPago &
                        x.NroCuota == nroCuota &
                        x.ImportePagado == null);
                db.Pagos.Remove(cGen);
                db.SaveChanges();
            }
        }

        internal static void GeneraNuevaCuota(int idPlanPago, int i, Curso curso)
        {
            using (var db = new SMPorresEntities())
            {
                var p = new Pago();
                p.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
                p.IdPlanPago = idPlanPago;
                p.NroCuota = (short)i;
                p.ImporteCuota = (i == 0) ? curso.ImporteMatricula : curso.ImporteCuota;
                p.Estado = (byte)EstadoPago.Impago;
                db.Pagos.Add(p);
                db.SaveChanges();
            }
        }

        internal static void GeneraNuevaCuotaDeMatricula(Pago p, decimal importeCuota)//idPlanPago, int i, decimal importeCuota, int idPago) //, Curso curso)
        {
            using (var db = new SMPorresEntities())
            {
                var pa = db.Pagos.Find(p.Id);
                pa.Estado = (byte)EstadoPago.Impago;
                pa.ImportePagoTermino = p.PlanPago.Curso.DescuentoMatricula;
                pa.ImporteCuota = importeCuota;
                db.SaveChanges();
            }
        }

        internal static void GeneraNuevaCuotaDeMatricula(int idPlanPago, int i, decimal importeCuota)
        {
            using (var db = new SMPorresEntities())
            {
                var p = new Pago();
                p.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
                p.IdPlanPago = idPlanPago;
                p.NroCuota = (short)i;
                p.ImporteCuota = importeCuota;
                p.Estado = (byte)EstadoPago.Impago;
                db.Pagos.Add(p);
                db.SaveChanges();
            }
        }

        internal static void GeneraNuevaCuotaDeMatricula(int idPlanPago, decimal importeCuota, int orden)//idPlanPago, int i, decimal importeCuota, int idPago) //, Curso curso)
        {
            using (var db = new SMPorresEntities())
            {
                var cGen = db.Pagos.Where(x =>
                        x.IdPlanPago == idPlanPago &
                        x.NroCuota == 0).ToList();

                int loop = 1;
                foreach (var item in cGen)
                {
                    if (loop == orden)
                    {
                        var pa = db.Pagos.Find(item.Id);
                        pa.Estado = (byte)EstadoPago.Impago;
                        pa.ImporteCuota = importeCuota;
                        pa.ImportePagoTermino = 0;
                        db.SaveChanges();
                    }
                    loop++;
                }

                if (cGen.Count() == 1 && orden == 2)    // crea la segunda cuota
                {
                    GeneraNuevaCuotaDeMatricula(idPlanPago, 0, importeCuota);
                }
                if (cGen.Count() == 2 && orden == 3)    // crea la tercer cuota
                {
                    //GeneraNuevaCuota(idPlanPago, 0, cGen.FirstOrDefault().PlanPago.Curso);
                    GeneraNuevaCuotaDeMatricula(idPlanPago, 0, importeCuota);
                }

            }
        }

        public static void EliminarCuotasGeneradasMatrícula(int nroCuota, int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.Pagos.Any(t => t.IdPlanPago == idPlanPago & t.NroCuota == nroCuota))
                {
                    throw new Exception("No existe la cuota generada " + nroCuota);
                }
                var cGen = db.Pagos.Where(x =>
                        x.IdPlanPago == idPlanPago &
                        x.NroCuota == nroCuota &
                        x.ImportePagado == null).ToList();
                foreach (var item in cGen)
                {
                    //db.Pagos.Remove(item);
                    var pa = db.Pagos.Find(item.Id);
                    pa.Estado = (byte)EstadoPago.Baja;
                    db.SaveChanges();
                }

            }
        }

        public static IList<Pago> ObtenerDeudaPorAlumno(decimal nroDocumento)
        {
            using (var db = new SMPorresEntities())
            {
                var qry = (from pp in db.PlanesPago
                           join a in db.Alumnos on pp.IdAlumno equals a.Id
                           join p in db.Pagos on pp.Id equals p.IdPlanPago
                           join ca in db.CursosAlumnos on new { pp.IdCurso, pp.IdAlumno } equals
                             new { ca.IdCurso, ca.IdAlumno }
                           join c in db.Cuotas on new { cl = ca.CicloLectivo, p.NroCuota } equals
                              new { cl = c.CicloLectivo.Value, c.NroCuota }
                           where
                               a.NroDocumento == nroDocumento &&
                               pp.Estado == (short)EstadoPlanPago.Vigente &&
                               c.VtoCuota <= Configuration.CurrentDate &&
                               p.ImportePagado == null //Impago
                           select p).ToList()
                        .Select(
                            pa => new Pago
                            {
                                Id = pa.Id,
                                NroCuota = pa.NroCuota,
                                ImporteCuota = pa.ImporteCuota,
                                ImportePagado = pa.ImportePagado
                            });
                return qry.ToList();
            }
        }

        public static int CantidadCuotasImpagasMatrícula(decimal idPlanPago)
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

        public static string ObtenerConcepto(Int32 idPlanPago, Pago pago)
        {
            string concepto = "";
            using (var db = new SMPorresEntities())
            {
                var cuotas = db.Pagos.Where(x => x.IdPlanPago == idPlanPago
                                            && x.NroCuota == 0
                                            && x.Estado != (short)EstadoPago.Baja)
                                            .OrderBy(x => x.Id);
                if (cuotas.Count() == 1)
                {
                    concepto = "Matrícula";
                }
                else
                {
                    short orden = 0;
                    foreach (var item in cuotas)
                    {
                        orden += 1;
                        if (item.Id == pago.Id)
                        {
                            concepto = "Matrícula Cuota Nº " + orden.ToString();
                        }
                    }
                }

            }
            return concepto;
        }
    }
}
