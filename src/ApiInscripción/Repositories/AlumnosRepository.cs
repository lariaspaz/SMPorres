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

                var trx = db.Database.BeginTransaction();
                try
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

                    var idCurso = db.Cursos.Where(c => c.IdCarrera == idCarrera).First().Id;
                    CursosAlumnosRepository.Insertar(idCarrera, id);
                    PlanesPagoRepository.Insertar(id, idCurso);

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
