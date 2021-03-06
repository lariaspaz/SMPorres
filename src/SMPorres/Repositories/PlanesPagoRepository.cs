﻿using SMPorres.Lib;
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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                                    FechaGrabacion = pp.FechaGrabacion,
                                    TipoBeca = pp.TipoBeca
                                }
                            );

                return (from pp in query orderby pp.Estado, pp.FechaGrabacion select pp).ToList();
            }
        }

        internal static PlanPago ObtenerPlanPagoPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.PlanesPago.Find(id);
            }
        }

        public static PlanPago Insertar(int idAlumno, int idCurso, short porcentajeBeca, int modalidad,
            TipoBeca tipoBeca)
        {
            using (var db = new SMPorresEntities())
            {
                if (db.PlanesPago.Any(pp => pp.IdAlumno == idAlumno && pp.IdCurso == idCurso & pp.Estado == (short)EstadoPlanPago.Vigente))
                {
                    throw new Exception("El alumno ya tiene un plan de pago vigente en el curso seleccionado.");
                }

                db.Database.Log = s => _log.Debug(s);
                using (var trx = db.Database.BeginTransaction())
                {
                    try
                    {
                        var plan = CrearPlanPago(db, idAlumno, idCurso, porcentajeBeca, modalidad, tipoBeca);
                        PagosRepository.CrearMatrícula(db, plan);
                        PagosRepository.CrearCuotas(db, plan, modalidad);
                        trx.Commit();
                        return plan;
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }

        private static PlanPago CrearPlanPago(SMPorresEntities db, int idAlumno, int idCurso,
            short porcentajeBeca, int modalidad, TipoBeca tipoBeca)
        {
            var curso = CursosRepository.ObtenerCursoPorId(idCurso);
            var plan = new PlanPago
            {
                Id = db.PlanesPago.Any() ? db.PlanesPago.Max(c1 => c1.Id) + 1 : 1,
                IdAlumno = idAlumno,
                IdCurso = idCurso,
                //Curso = curso,
                CantidadCuotas = CursosRepository.ObtieneMaxCuota(modalidad),//Configuration.MaxCuotas,
                NroCuota = CursosRepository.ObtieneMinCuota(modalidad), //1,
                ImporteCuota = curso.ImporteCuota,
                PorcentajeBeca = porcentajeBeca,
                TipoBeca = (byte)tipoBeca,
                Estado = (short)EstadoPlanPago.Vigente,
                IdUsuarioEstado = Session.CurrentUser.Id,
                FechaGrabacion = Configuration.CurrentDate,
                IdUsuario = Session.CurrentUser.Id,
                Modalidad = modalidad //curso.Modalidad
            };
            db.PlanesPago.Add(plan);
            db.SaveChanges();
            return plan;
        }

        public static PlanPago Actualizar(int planDePagoId, short porcentajeBeca, TipoBeca tipoBeca)
        {
            using (var db = new SMPorresEntities())
            {
                var p = db.PlanesPago.Find(planDePagoId);
                p.PorcentajeBeca = porcentajeBeca;
                p.TipoBeca = (byte)tipoBeca;
                db.SaveChanges();
                return p;
            }
        }

        public static void Actualizar(int idCurso, decimal importeCuota)
        {
            var ciclo = ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo;
            using (var db = new SMPorresEntities())
            {
                var planes = from pp in db.PlanesPago
                             join ca in db.CursosAlumnos on
                                new { pp.IdCurso, pp.IdAlumno } equals new { ca.IdCurso, ca.IdAlumno }
                             where pp.IdCurso == idCurso &&
                                    pp.Estado == (short)EstadoPlanPago.Vigente &&
                                    ca.CicloLectivo == ciclo
                             select pp;
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
                pp.Estado = (int)Models.EstadoPlanPago.Baja;
                db.SaveChanges();
            }
        }

        internal static void ActualizarModalidad(int idPlanPago, string nombreCurso, int modalidad)
        {
            using (var db = new SMPorresEntities())
            {
                int anual = 1;
                int primerCuatrimestre = 2;
                int segundoCuatrimestre = 3;
                int sinCursado = 4;
                bool actualizaCuotas = false;

                int idCurso = db.PlanesPago.Find(idPlanPago).IdCurso;
                var curso = db.Cursos.Find(idCurso);

                var modActual = db.PlanesPago.Find(idPlanPago).Modalidad;
                if (modActual == modalidad) return;

                if (modActual == anual)
                {
                    if (modalidad == primerCuatrimestre & !PagosRegistrados(idPlanPago, segundoCuatrimestre))
                    {
                        EliminaCuotas(segundoCuatrimestre, idPlanPago);
                        actualizaCuotas = true;
                    }
                    if (modalidad == segundoCuatrimestre & !PagosRegistrados(idPlanPago, primerCuatrimestre))
                    {
                        EliminaCuotas(primerCuatrimestre, idPlanPago);
                        actualizaCuotas = true;
                    }
                    if (modalidad == sinCursado & !PagosRegistrados(idPlanPago, anual))
                    {
                        EliminaCuotas(anual, idPlanPago);
                        actualizaCuotas = true;
                    }
                }

                if (modActual == primerCuatrimestre)
                {
                    if (modalidad == anual)
                    {
                        GeneraCuotas(segundoCuatrimestre, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                    if (modalidad == segundoCuatrimestre & !PagosRegistrados(idPlanPago, primerCuatrimestre))
                    {
                        EliminaCuotas(primerCuatrimestre, idPlanPago);
                        GeneraCuotas(segundoCuatrimestre, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                    if (modalidad == sinCursado & !PagosRegistrados(idPlanPago, primerCuatrimestre))
                    {
                        EliminaCuotas(primerCuatrimestre, idPlanPago);
                        actualizaCuotas = true;
                    }
                }

                if (modActual == segundoCuatrimestre)
                {
                    if (modalidad == anual & !PagosRegistrados(idPlanPago, segundoCuatrimestre))
                    {
                        GeneraCuotas(primerCuatrimestre, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                    if (modalidad == primerCuatrimestre & !PagosRegistrados(idPlanPago, segundoCuatrimestre))
                    {
                        EliminaCuotas(segundoCuatrimestre, idPlanPago);
                        GeneraCuotas(primerCuatrimestre, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                    if (modalidad == sinCursado & !PagosRegistrados(idPlanPago, segundoCuatrimestre))
                    {
                        EliminaCuotas(segundoCuatrimestre, idPlanPago);
                        actualizaCuotas = true;
                    }
                }

                if (modActual == sinCursado)
                {
                    if (modalidad == anual)
                    {
                        GeneraCuotas(anual, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                    if (modalidad == primerCuatrimestre)
                    {
                        GeneraCuotas(primerCuatrimestre, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                    if (modalidad == segundoCuatrimestre)
                    {
                        GeneraCuotas(segundoCuatrimestre, idPlanPago, curso);
                        actualizaCuotas = true;
                    }
                }

                if (actualizaCuotas)
                {
                    var pp = db.PlanesPago.Find(idPlanPago);
                    pp.Modalidad = modalidad;
                    db.SaveChanges();

                    PlanesPagoRepository.ActualizarNroyCantidadCuotas(idPlanPago, modalidad);
                }
            }
        }

        private static void GeneraCuotas(int modalidad, int idPlanPago, Curso curso)
        {
            var minC = CursosRepository.ObtieneMinCuota(modalidad);
            var maxC = CursosRepository.ObtieneMaxCuota(modalidad);
            for (int i = minC; i <= maxC; i++)
            {
                PagosRepository.GeneraNuevaCuota(idPlanPago, i, curso);
            }
        }

        private static void EliminaCuotas(int? modActual, int idPlanPago)
        {
            var minC = CursosRepository.ObtieneMinCuota(modActual);
            var maxC = CursosRepository.ObtieneMaxCuota(modActual);

            for (int i = minC; i <= maxC; i++)
            {
                PagosRepository.EliminarCuotaGenerada(i, idPlanPago);
            }
        }

        private static bool PagosRegistrados(int planPago, int modalidad)
        {
            bool pagos = false;
            var minC = CursosRepository.ObtieneMinCuota(modalidad);
            var maxC = CursosRepository.ObtieneMaxCuota(modalidad);

            using (var db = new SMPorresEntities())
            {
                var p = db.Pagos.Where(x =>
                    x.IdPlanPago == planPago &
                    x.ImportePagado > 0 &
                    x.NroCuota >= minC &
                    x.NroCuota <= maxC);
                if (p.Any()) pagos = true;
            }
            return pagos;
        }

        internal static void ActualizarNroyCantidadCuotas(int id, int modalidad)
        {
            using (var db = new SMPorresEntities())
            {
                var pp = db.PlanesPago.Find(id);
                pp.NroCuota = CursosRepository.ObtieneMinCuota(modalidad);
                pp.CantidadCuotas = CursosRepository.ObtieneMaxCuota(modalidad);
                db.SaveChanges();
            }
        }
    }
}
