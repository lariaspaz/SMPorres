using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models.ViewModels
{
    public class PermisoExámen
    {
        public string AlumnoApellido { get; set; }

        public string AlumnoNombre { get; set; }

        public string Curso { get; set; }

        public string Carrera { get; set; }

        public DateTime PróximaCuota { get; set; }
    }
}