using Consultas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Repositories
{
    public class CursoAlumnoRepository
    {
        public void Actualizar(Models.WebServices.CursoAlumno cursoAlumno)
        {
            using (var db = new SMPorres_DevEntities())
            {
                //var a = db.AlumnoWebs.Find(alumno.Id);
                //bool insertar = a == null;
                //if (insertar)
                //{
                //    a = new AlumnoWeb();
                //}
                //a.Id = alumno.Id;
                //a.Nombre = alumno.Nombre;
                //a.Apellido = alumno.Apellido;
                //a.TipoDocumento = alumno.TipoDocumento;
                //a.NroDocumento = alumno.NroDocumento;
                //a.Estado = (byte)alumno.Estado;
                //if (insertar)
                //{
                //    db.AlumnoWebs.Add(a);
                //}
            }
        }
    }
}