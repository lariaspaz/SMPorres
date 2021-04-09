using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    public class AlumnosRepository
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Insertar(string nombre, string apellido, int idTipoDocumento, decimal nroDocumento,
            DateTime fechaNacimiento, string email, string dirección, char sexo, int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                ValidarDatos(db, nombre, apellido, idTipoDocumento, nroDocumento, fechaNacimiento,
                    email, dirección, sexo, idCarrera);
                var idCurso = CursosRepository.ObtenerCursoInicial(idCarrera);
                Alumno a;
                var trx = db.Database.BeginTransaction();
                try
                {
                    a = GrabarAlumno(db, nombre, apellido, idTipoDocumento, nroDocumento, fechaNacimiento,
                        email, dirección, sexo);
                    CursosAlumnosRepository.Insertar(db, idCurso, a.Id);
                    PlanesPagoRepository.Insertar(db, a.Id, idCurso);
                    trx.Commit();
                    _log.Info($"Se ha insertado el alumno {a.Id}.");
                }
                catch (Exception ex)
                {
                    trx.Rollback();
                    throw new Exception("Error al grabar la inscripción.", ex);
                }

                try
                {
                    SubirAWeb(db, a.Id);
                    _log.Info("Se han subido los datos del alumno a la web.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al subir los datos a la web.", ex);
                }
            }
        }

        private static void SubirAWeb(SMPorresEntities db, int id)
        {
            var uploadData = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["UploadData"]);
            if (uploadData)
            {
                var alumnoWeb = new WebRepository().ObtenerAlumno(db, id);
                var interés = ConfiguracionRepository.ObtenerConfiguracion().InteresPorMora;
                if (!new Lib.WebServices.Consultas().SubirAlumno(alumnoWeb, interés))
                {
                    throw new System.Web.HttpException("Se grabaron los datos del alumno pero no se pudo actualizar sus datos en la web.");
                }
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
                throw new ValidationException("El nombre y el apellido son incorrectos.");
            }
            if (db.Alumnos.Any(a => a.IdTipoDocumento == idTipoDocumento && a.NroDocumento == nroDocumento))
            {
                throw new ValidationException("Ya existe un alumno con este número de documento.");
            }
            if (!db.TiposDocumento.Any(td => td.Id == idTipoDocumento))
            {
                throw new ValidationException("El tipo de documento es incorrecto.");
            }
            if (idCarrera == 0 || !db.Carreras.Any(c => c.Id == idCarrera))
            {
                throw new ValidationException("La carrera es incorrecta.");
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
