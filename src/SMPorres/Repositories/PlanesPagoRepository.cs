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
        internal static List<PlanPago> ObtenerPlanesPagoPorAlumnoYCurso1(int idAlumno, int idCurso)
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

        public static IEnumerable<PlanPago> ObtenerPlanesPagoPorAlumnoYCurso(int idAlumno, int idCurso)
        {
            using (var db = new SMPorresEntities())
            {
                return (from pp in db.PlanesPago
                        where pp.IdAlumno == idAlumno && pp.IdCurso == idCurso
                        select pp).ToList();
            }
        }

        public static PlanPago Insertar(int idAlumno, int idCurso, short porcentajeBeca)
        {
            using (var db = new SMPorresEntities())
            {
                var id = db.PlanesPago.Any() ? db.PlanesPago.Max(c1 => c1.Id) + 1 : 1;                
                var pp = new PlanPago
                {
                    Id = id,
                    IdAlumno = idAlumno,
                    IdCurso = idCurso,
                    CantidadCuotas = 10,
                    NroCuota = 1,
                    ImporteCuota = 0,
                    PorcentajeBeca = porcentajeBeca,
                    Estado = 1,
                    IdUsuarioEstado = Session.CurrentUser.Id,
                    FechaGrabacion = Configuration.CurrentDate,
                    IdUsuario = Session.CurrentUser.Id
                };
                db.PlanesPago.Add(pp);
                db.SaveChanges();
                return pp;
            }
        }
    }
}
