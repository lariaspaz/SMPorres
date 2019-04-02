using Consultas.Lib;
using Consultas.Models;
using Consultas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class CursosAlumnosRepository
    {
        public CursoAlumnoWeb Actualizar(SMPorresEntities db, int idAlumnoWeb,
            Models.WebServices.CursoAlumno cursoAlumno)
        {
            var ca = db.CursosAlumnosWeb.Find(cursoAlumno.Id);
            bool insertar = ca == null;
            if (insertar)
            {
                ca = new CursoAlumnoWeb();
                ca.Id = cursoAlumno.Id;
                ca.IdAlumnoWeb = idAlumnoWeb;
            }
            ca.IdCurso = cursoAlumno.IdCurso;
            ca.Curso = cursoAlumno.Curso;
            ca.IdCarrera = cursoAlumno.IdCarrera;
            ca.Carrera = cursoAlumno.Carrera;
            if (insertar)
            {
                db.CursosAlumnosWeb.Add(ca);
            }
            db.SaveChanges();
            return ca;
        }

        public IEnumerable<Carrera> ObtenerCarreras()
        {
            using (var db = new SMPorresEntities())
            {
                return (from ca in db.CursosAlumnosWeb
                        select new Carrera
                        {
                            Id = ca.IdCarrera,
                            Descripción = ca.Carrera
                        })
                        .Distinct()
                        .ToList();
            }
        }

        public IEnumerable<Curso> ObtenerCursos(int idCarrera)
        {
            using (var db = new SMPorresEntities())
            {
                return (from ca in db.CursosAlumnosWeb
                        where ca.IdCarrera == idCarrera
                        select new Curso
                        {
                            Id = ca.IdCurso,
                            Descripción = ca.Curso
                        })
                        .Distinct()
                        .ToList();
            }
        }

        public IEnumerable<CursoAlumnoWeb> ObtenerCursosAlumno()
        {
            using (var db = new SMPorresEntities())
            {
                return (from ca in db.CursosAlumnosWeb
                        where ca.AlumnoWeb.Id == Session.CurrentUserId
                        select new {
                            ca.Id,
                            ca.Carrera,
                            ca.Curso })
                        .Distinct()
                        .ToList()
                        .Select(ca => new CursoAlumnoWeb
                        {
                            Id = ca.Id,
                            Carrera = ca.Carrera,
                            Curso = ca.Curso
                        })
                        .ToList();
            }
        }

        public CursoAlumnoWeb ObtenerCursoAlumnoPorId(int id)
        {
            using (var db = new SMPorresEntities())
            {
                return db.CursosAlumnosWeb.Find(id);
            }
        }
    }
}