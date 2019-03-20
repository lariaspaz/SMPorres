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
            using (var db = new SMPorres_DevEntities())
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
                if (insertar)
                {
                    db.AlumnoWebs.Add(a);
                }
            }
        }
    }
}