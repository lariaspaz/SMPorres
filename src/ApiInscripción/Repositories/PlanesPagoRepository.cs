using ApiInscripción.Lib;
using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    class PlanesPagoRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static PlanPago Insertar(int idAlumno, int idCurso)
        {
            _log.Debug("Creando plan de pago");

            if (idAlumno == 0)
            {
                throw new Exception("No se pudo determinar el alumno.");
            }
            if (idCurso == 0)
            {
                throw new Exception("No se pudo determinar el curso.");
            }

            using (var db = new SMPorresEntities())
            {
                if (db.PlanesPago.Any(pp => pp.IdAlumno == idAlumno && pp.IdCurso == idCurso & pp.Estado == (short)EstadoPlanPago.Vigente))
                {
                    throw new Exception("El alumno ya tiene un plan de pago vigente en este curso.");
                }

                var curso = CursosRepository.ObtenerCursoPorId(idCurso);
                var id = db.PlanesPago.Any() ? db.PlanesPago.Max(c1 => c1.Id) + 1 : 1;
                var trx = db.Database.BeginTransaction();
                var plan = new PlanPago
                {
                    Id = id,
                    IdAlumno = idAlumno,
                    IdCurso = idCurso,
                    CantidadCuotas = CursosRepository.ObtieneMaxCuota(curso.Modalidad),//Configuration.MaxCuotas,
                    NroCuota = CursosRepository.ObtieneMinCuota(curso.Modalidad), //1,
                    ImporteCuota = curso.ImporteCuota,
                    PorcentajeBeca = 0,
                    TipoBeca = (byte)TipoBeca.AplicaHastaVto,
                    Estado = (short)EstadoPlanPago.Vigente,
                    IdUsuarioEstado = Session.CurrentUser.Id,
                    FechaGrabacion = Configuration.CurrentDate,
                    IdUsuario = Session.CurrentUser.Id,
                    Modalidad = curso.Modalidad
                };
                _log.Debug("Generando pagos");
                try
                {
                    db.PlanesPago.Add(plan);
                    db.SaveChanges();
                    //carga cuota matricula
                    var pm = new Pago();
                    pm.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
                    pm.IdPlanPago = id;
                    pm.NroCuota = 0;
                    pm.ImporteCuota = curso.ImporteMatricula;
                    pm.Estado = (short) EstadoPago.Impago;
                    db.Pagos.Add(pm);
                    db.SaveChanges();

                    //leer modalidad y obtener minCuota y maxCuota
                    short minC = CursosRepository.ObtieneMinCuota(curso.Modalidad);
                    short maxC = CursosRepository.ObtieneMaxCuota(curso.Modalidad);
                    if(minC != maxC)
                    {
                    //for (short i = 0; i <= Configuration.MaxCuotas; i++)
                        for (short i = minC; i <= maxC; i++)
                        {
                            var p = new Pago();
                            p.Id = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.Id) + 1 : 1;
                            p.IdPlanPago = id;
                            p.NroCuota = i;
                            p.ImporteCuota = (i == 0) ? curso.ImporteMatricula : curso.ImporteCuota;
                            pm.Estado = (short)EstadoPago.Impago;
                            db.Pagos.Add(p);
                            db.SaveChanges();
                        }
                    }
                    trx.Commit();
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex);
                    trx.Rollback();
                    throw;
                }
                return plan;
            }
        }
    }
}
