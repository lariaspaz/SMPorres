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

        public static PlanPago Insertar(SMPorresEntities db, int idAlumno, int idCurso)
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

            if (db.PlanesPago.Any(pp => pp.IdAlumno == idAlumno && pp.IdCurso == idCurso & pp.Estado == (short)EstadoPlanPago.Vigente))
            {
                throw new Exception("El alumno ya tiene un plan de pago vigente en este curso.");
            }

            var curso = CursosRepository.ObtenerCursoPorId(idCurso);
            if (curso == null)
            {
                throw new Exception("No se encontró el curso " + idCurso);
            }

            var id = db.PlanesPago.Any() ? db.PlanesPago.Max(c1 => c1.Id) + 1 : 1;
            //var trx = db.Database.BeginTransaction();
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

                PagosRepository.InsertarMatricula(db, curso, id);
                
                PagosRepository.InsertarPagosCuotas(db, curso, id);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                //trx.Rollback();
                throw;
            }
            return plan;
        }

        
    }
}
