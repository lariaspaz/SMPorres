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
        internal static List<PlanPago> ObtenerPlanesPagoPorAlumnoYCurso(int idAlumno, int idCurso)
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
                    CantidadCuotas = 10,
                    NroCuota = 1,
                    ImporteCuota = curso.ImporteCuota,
                    PorcentajeBeca = porcentajeBeca,
                    Estado = (short)EstadoPlanPago.Vigente,
                    IdUsuarioEstado = Session.CurrentUser.Id,
                    FechaGrabacion = Configuration.CurrentDate,
                    IdUsuario = Session.CurrentUser.Id
                };
                db.PlanesPago.Add(plan);
                for (short i = 1; i <= Configuration.MaxCuotas; i++)
                {
                    var p = new Pago();
                    p.IdPlanPago = id;
                    p.NroCuota = i;
                    p.ImporteCuota = curso.ImporteCuota;
                    db.Pagos.Add(p);
                }
                try
                {
                    db.SaveChanges();
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
    }
}
