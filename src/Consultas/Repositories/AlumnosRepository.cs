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
            using (var db = new SMPorresEntities())
            {
                var trx = db.Database.BeginTransaction();
                try
                {
                    var a = db.AlumnoWebs.Find(alumno.Id);
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
                    if (insertar)
                    {
                        db.AlumnoWebs.Add(a);
                    }
                    db.SaveChanges();

                    var caRepo = new CursoAlumnoRepository();
                    var pagosRepo = new PagosRepository();
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
                var a = db.AlumnoWebs.Find(idAlumno);
                if (a == null) return false;
                a.Contraseña = pwd;
                db.SaveChanges();
                return true;
            }
        }

        public bool ExisteAlumno(int nrodoc, string contraseña)
        {
            string hashpwd = "";
            using (var alg = System.Security.Cryptography.SHA512.Create())
            {
                alg.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contraseña));
                hashpwd = BitConverter.ToString(alg.Hash);
            }

            using (var db = new SMPorresEntities())
            {
                return (from a in db.AlumnoWebs
                            where nrodoc == a.NroDocumento && hashpwd == a.Contraseña
                            select a).Any();
            }
        }

        public AlumnoWeb ObtenerAlumno(int nrodoc)
        {
            using (var db = new SMPorresEntities())
            {
                var alumno = (from a in db.AlumnoWebs where nrodoc == a.NroDocumento select a).FirstOrDefault();
                if (alumno != null)
                {
                    db.Entry(alumno).Reference(r => r.RolUsuarioWeb).Load();
                }
                return alumno;
            }
        }
    }
}