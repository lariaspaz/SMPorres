using Consultas.Models.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models.WebServices
{
    public class Alumno
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string TipoDocumento { get; set; }

        public decimal NroDocumento { get; set; }

        public List<CursoAlumno> CursosAlumnos { get; set; }

        public int Estado { get; set; }
    }
}