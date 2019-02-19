using SMPorres.Lib;
using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class PlanesPagoRepository
    {
        internal static List<PlanPago> ObtenerPlanesPago(int idAlumno, int idCurso)
        {
            using (var db = new SMPorresEntities())
            {
                var query = (from pp in db.PlanesPago
                             where pp.IdAlumno == idAlumno && pp.IdCurso == idCurso
                             select pp)
                            .ToList()
                            .Select(
                                pp => new PlanPago
                                {
                                    Id = pp.Id,
                                    CantidadCuotas = pp.CantidadCuotas,
                                    NroCuota = pp.NroCuota,
                                    ImporteCuota = pp.ImporteCuota,
                                    PorcentajeBeca = pp.PorcentajeBeca,
                                    Estado = pp.Estado,
                                    FechaGrabacion = pp.FechaGrabacion
                                }
                            );

                return (from pp in query orderby pp.Estado, pp.FechaGrabacion select pp).ToList();
            }
        }

        internal static PlanPago ObtenerPlanPago(int idPago)
        {
            using (var db = new SMPorresEntities())
            {
                return db.Pagos.Find(idPago).PlanPago;
                //var pago = db.Pagos.Find(idPago).PlanPago;
                //var query = (from pp in pago.PlanPago
                //             where pp.IdAlumno == idAlumno && pp.IdCurso == idCurso
                //             select pp)
                //            .ToList()
                //            .Select(
                //                pp => new PlanPago
                //                {
                //                    Id = pp.Id,
                //                    CantidadCuotas = pp.CantidadCuotas,
                //                    NroCuota = pp.NroCuota,
                //                    ImporteCuota = pp.ImporteCuota,
                //                    PorcentajeBeca = pp.PorcentajeBeca,
                //                    Estado = pp.Estado,
                //                    FechaGrabacion = pp.FechaGrabacion
                //                }
                //            );

                //return (from pp in query orderby pp.Estado, pp.FechaGrabacion select pp).ToList();
            }
        }

        public static PlanPago Insertar(int idAlumno, int idCurso, short porcentajeBeca)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.PlanesPago.Any(pp => pp.IdAlumno == idAlumno && pp.IdCurso == idCurso & pp.Estado == (short)EstadoPlanPago.Vigente))
                {
                    throw new Exception("El alumno ya tiene un plan de pago vigente en el curso seleccionado.");
                }

                var curso = CursosRepository.ObtenerCursoPorId(idCurso);
                var id = db.PlanesPago.Any() ? db.PlanesPago.Max(c1 => c1.Id) + 1 : 1;
                var trx = db.Database.BeginTransaction();
                var plan = new PlanPago
                {
                    Id = id,
                    IdAlumno = idAlumno,
                    IdCurso = idCurso,
                    CantidadCuotas = Configuration.MaxCuotas,
                    NroCuota = 1,
                    ImporteCuota = curso.ImporteCuota,
                    PorcentajeBeca = porcentajeBeca,
                    Estado = (short)EstadoPlanPago.Vigente,
                    IdUsuarioEstado = Session.CurrentUser.Id,
                    FechaGrabacion = Configuration.CurrentDate,
                    IdUsuario = Session.CurrentUser.Id
                };
                try
                {
                    db.PlanesPago.Add(plan);
                    db.SaveChanges();
                    for (short i = 0; i <= Configuration.MaxCuotas; i++)
                    {
                        var p = new Pago();
                        p.IdPago = db.Pagos.Any() ? db.Pagos.Max(p1 => p1.IdPago) + 1 : 1;
                        p.IdPlanPago = id;
                        p.NroCuota = i;
                        p.ImporteCuota = (i == 0) ? curso.ImporteMatricula : curso.ImporteCuota;
                        db.Pagos.Add(p);
                        db.SaveChanges();
                    }
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
                return plan;
            }
        }

        public static void Actualizar(int idCurso, decimal importeCuota)
        {
            using (var db = new SMPorresEntities())
            {
                var planes = from p in db.PlanesPago where p.IdCurso == idCurso && p.Estado == (short)EstadoPlanPago.Vigente select p;
                foreach (var p in planes)
                {
                    p.ImporteCuota = importeCuota;
                }
                db.SaveChanges();
            }
        }

        public static void AnularPlanDePago(int id)
        {
            using (var db = new SMPorresEntities())
            {
                if (!db.PlanesPago.Any(t => t.Id == id))
                {
                    throw new Exception(String.Format("No existe plan de pago {0}", id));
                }
                var pp = db.PlanesPago.Find(id);
                pp.Estado = 3;
                
                db.SaveChanges();
            }
        }
    }
}
