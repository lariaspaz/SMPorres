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
        public struct DetallePago
        {
            public decimal DescuentoPagoTérmino { get; internal set; }
            public decimal RecargoPorMora { get; internal set; }
            public decimal Beca { get; internal set; }
            public decimal ImporteBase { get; internal set; }
            public bool Resultado { get; internal set; }
            public decimal TotalAPagar { get; internal set; }
        }

        private void Insertar(int idPlanPago)
        {
            using (var db = new SMPorresEntities())
            {
                var pp = db.PlanesPago.Find(idPlanPago);
                for (short i = 1; i <= Lib.Configuration.MaxCuotas; i++)
                {
                    var p = new Pago();
                    p.IdPlanPago = idPlanPago;
                    p.NroCuota = i;
                    p.ImporteCuota = pp.ImporteCuota;
                    db.Pagos.Add(p);
                }
            }
        }

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
                var query = (from p in db.Pagos where p.IdPlanPago == idPlanPago select p)
                            .ToList()
                            .Select(
                                p => new Pago
                                {
                                    Id = p.Id,
                                    NroCuota = p.NroCuota,
                                    ImporteCuota = p.ImporteCuota,
                                    Fecha = p.Fecha
                                }
                            );                    
                return query.OrderBy(p => p.NroCuota).ToList();
            }
        }

        internal static Pago ObtenerPago(int idPago)
        {
            using (var db = new SMPorresEntities())
            {
                var p = db.Pagos.Find(idPago);
                db.Entry(p).Reference(p1 => p1.PlanPago).Load();
                db.Entry(p.PlanPago).Reference(pp => pp.Alumno).Load();
                db.Entry(p.PlanPago.Alumno).Reference(a => a.TipoDocumento).Load();
                db.Entry(p.PlanPago).Reference(pp => pp.Curso).Load();
                db.Entry(p.PlanPago.Curso).Reference(c => c.Carrera).Load();
                return p;
            }
        }

        //public static DetallePago ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
        //{
        //    DetallePago dp = new DetallePago();
        //    var pago = ObtenerPago(idPago);
        //    dp.ImporteBase = pago.ImporteCuota;
        //    var descBeca = (decimal)pago.PlanPago.PorcentajeBeca;
        //    dp.Beca = 0;
        //    if (descBeca > 0)
        //    {
        //        dp.Beca = Math.Round(dp.ImporteBase * (descBeca / 100));
        //    }

        //    if (pago.NroCuota == 0)
        //    {
        //        dp.TotalAPagar = dp.ImporteBase - dp.Beca;
        //        dp.Resultado = true;
        //        return dp;
        //    }

        //    var cuota = CuotasRepository.ObtenerCuotas().Where(c => c.NroCuota == pago.NroCuota).FirstOrDefault();
        //    if (cuota == null)
        //    {
        //        dp.Resultado = false;
        //        return dp;
        //    }

        //    var vtoCuota = cuota.VtoCuota;
        //    dp.TotalAPagar = 0;
        //    var impBecado = dp.ImporteBase - dp.Beca;
        //    if (fechaCompromiso <= vtoCuota)
        //    {
        //        var dpt = (decimal)(ConfiguracionRepository.ObtenerConfiguracion().DescuentoPagoTermino / 100);
        //        dp.DescuentoPagoTérmino = Math.Round(impBecado * dpt, 2);
        //        dp.TotalAPagar = dp.ImporteBase - dp.Beca - dp.DescuentoPagoTérmino;
        //    }
        //    else
        //    {
        //        var porcRecargo = (ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
        //        var díasAtraso = Math.Truncate((fechaCompromiso - vtoCuota).TotalDays);
        //        var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
        //        dp.RecargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);
        //        dp.TotalAPagar = dp.ImporteBase - dp.Beca + dp.RecargoPorMora;
        //    }

        //    dp.Resultado = true;
        //    return dp;
        //}

        public static Pago ObtenerDetallePago(int idPago, DateTime fechaCompromiso)
        {
            Pago pago = ObtenerPago(idPago);

            var impBase = pago.ImporteCuota;
            if (pago.NroCuota == 0)
            {
                return pago;
            }

            var descBeca = (decimal)pago.PlanPago.PorcentajeBeca;
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
            if (fechaCompromiso <= pago.FechaVto)
            {
                var dpt = (decimal)(ConfiguracionRepository.ObtenerConfiguracion().DescuentoPagoTermino / 100);
                var descPagoTérmino = Math.Round(impBecado * dpt, 2);

                totalAPagar = impBase - beca - descPagoTérmino;

                pago.PorcDescPagoTermino = (double) Math.Truncate(dpt * 100);
                pago.ImportePagoTermino = descPagoTérmino;
            }
            else
            {
                var porcRecargo = (ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora / 100) / 30.0;
                var díasAtraso = Math.Truncate((fechaCompromiso - pago.FechaVto.Value).TotalDays);
                var porcRecargoTotal = (decimal)(porcRecargo * díasAtraso);
                var recargoPorMora = Math.Round(impBecado * porcRecargoTotal, 2);

                totalAPagar = impBase - beca + recargoPorMora;

                pago.PorcRecargo = porcRecargo;
                pago.ImporteRecargo = recargoPorMora;
            }

            pago.ImportePagado = totalAPagar;
            pago.PorcBeca = (double) descBeca;
            pago.ImporteBeca = beca;
            pago.IdBecaAlumno = null;

            return pago;
        }

        public static bool PagarCuota(int idPago, DetallePago detalle)
        {
            //[Id]                  
            //[IdPlanPago]          
            //[Fecha]               
            //[NroCuota]            
            //[ImportePagado]       
            //[ImporteCuota]        
            //[PorcDescPagoTermino] 
            //[ImportePagoTermino]  
            //[PorcentajeBeca]      
            //[IdBecaAlumno]        
            //[Recargo]             
            //[IdMedioPago]         
            //[IdArchivo]           
            //[EsContrasiento]      
            //[IdPagoAsiento]       
            //[IdUsuario]           
            //[FechaGrabacion]      


            var fecha = Lib.Configuration.CurrentDate;
            using (var db = new SMPorresEntities())
            {
                var p = db.Pagos.Find(idPago);
                p.IdUsuario = Session.CurrentUser.Id;
                p.FechaGrabacion = Configuration.CurrentDate;
                db.SaveChanges();
            }
            return true;
        }

    }
}
