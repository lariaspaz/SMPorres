using ApiInscripción.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiInscripción.Repositories
{
    class AlumnosRepository
    {
        public static Alumno Insertar(string nombre, string apellido, int idTipoDocumento, decimal nroDocumento,
            DateTime fechaNacimiento, string email, string dirección, char sexo)
        {
            using (var db = new SMPorresEntities())
            {
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
                        Sexo = sexo.ToString()
                    };
                    db.Alumnos.Add(a);
                    db.SaveChanges();
                    trx.Commit();
                    return a;
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        public static string GenerarContraseña(int idAlumno, ref string pwdEncriptada)
        {
            using (var db = new SMPorresEntities())
            {
                var a = db.Alumnos.Find(idAlumno);
                var pwd = Lib.Security.Cryptography.GenerarContraseña();
                pwdEncriptada = Lib.Security.Cryptography.CalcularSHA512(pwd);
                a.Contraseña = pwdEncriptada;
                db.SaveChanges();
                return pwd;
            }
        }
    }
}
