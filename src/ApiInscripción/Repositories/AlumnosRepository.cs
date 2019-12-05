using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    public class AlumnosRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Alumno Insertar(string nombre, string apellido, int idTipoDocumento, decimal nroDocumento,
            DateTime fechaNacimiento, string email, string dirección, char sexo, int idCarrera)
        {
            _log.Debug("Insertando alumno");
            using (var db = new SMPorresEntities())
            {
                ValidarDatos(db, nombre, apellido, idTipoDocumento, nroDocumento, fechaNacimiento,
                    email, dirección, sexo, idCarrera);
                var idCurso = CursosRepository.ObtenerCursoInicial(idCarrera);

                var trx = db.Database.BeginTransaction();
                try
                {
                    Alumno a = GrabarAlumno(db, nombre, apellido, idTipoDocumento, nroDocumento, fechaNacimiento,
                        email, dirección, sexo);
                    CursosAlumnosRepository.Insertar(db, idCurso, a.Id);
                    PlanesPagoRepository.Insertar(db, a.Id, idCurso);
                    SubirAWeb(db, a.Id);
                    trx.Commit();

                    return a;
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message);
                    trx.Rollback();
                    throw;
                }
            }
        }

        private static void SubirAWeb(SMPorresEntities db, int id)
        {
            var alumnoWeb = new WebRepository().ObtenerAlumno(db, id);
            var interés = ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora;
            if (!new Lib.WebServices.Consultas().SubirAlumno(alumnoWeb, interés))
            {
                throw new Exception("No se pudo actualizar el alumno en la web");
            }
        }

        private static Alumno GrabarAlumno(SMPorresEntities db, string nombre, string apellido,
            int idTipoDocumento, decimal nroDocumento, DateTime fechaNacimiento, string email, string dirección,
            char sexo)
        {
            var id = db.Alumnos.Any() ? db.Alumnos.Max(a1 => a1.Id) + 1 : 1;
            var a = new Alumno
            {
                Id = id,
                Nombre = nombre,
                Apellido = apellido,
                IdTipoDocumento = idTipoDocumento,
                NroDocumento = nroDocumento,
                FechaNacimiento = fechaNacimiento,
                EMail = email,
                Direccion = dirección,
                IdDomicilio = null,
                Estado = 1,
                Sexo = sexo.ToString(),
                Contraseña = GenerarContraseña(id, nroDocumento)
            };
            db.Alumnos.Add(a);
            db.SaveChanges();
            return a;
        }

        private static void ValidarDatos(SMPorresEntities db, string nombre, string apellido,
            int idTipoDocumento, decimal nroDocumento, DateTime fechaNacimiento, string email,
            string dirección, char sexo, int idCarrera)
        {
            _log.Debug("Validando datos");

            if (String.IsNullOrEmpty(nombre.Trim()) || String.IsNullOrEmpty(apellido.Trim()))
            {
                throw new Exception("El nombre y el apellido son incorrectos.");
            }
            if (db.Alumnos.Any(a => a.IdTipoDocumento == idTipoDocumento && a.NroDocumento == nroDocumento))
            {
                throw new Exception("Ya existe un alumno con este número de documento. "
                    + $"[tipdoc = {idTipoDocumento}, nrodoc = {nroDocumento}]");
            }
            if (!db.TiposDocumento.Any(td => td.Id == idTipoDocumento))
            {
                throw new Exception("El tipo de documento es incorrecto.");
            }
            if (idCarrera == 0 || !db.Carreras.Any(c => c.Id == idCarrera))
            {
                throw new Exception("La carrera es incorrecta.");
            }
        }

        public static string GenerarContraseña(int idAlumno, decimal nrodoc)
        {
            _log.Debug("Generando contraseña");

            string pwd = String.Join("", nrodoc.ToString().ToCharArray().Reverse().Take(6).Reverse());
            var pwdEncriptada = Lib.Security.Cryptography.CalcularSHA512(pwd);
            return pwdEncriptada;
        }
    }
}
