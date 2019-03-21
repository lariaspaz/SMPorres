using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class CursoAlumnoRepository
    {
        public CursoAlumnoWeb Actualizar(SMPorres_DevEntities db, int idAlumnoWeb, 
            Models.WebServices.CursoAlumno cursoAlumno)
        {
            var ca = db.CursoAlumnoWebs.Find(cursoAlumno.Id);
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
                db.CursoAlumnoWebs.Add(ca);
            }
            db.SaveChanges();
            return ca;
        }
    }
}