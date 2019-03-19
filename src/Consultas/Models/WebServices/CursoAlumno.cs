using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models.WebServices
{
    public class CursoAlumno
    {
        public int IdCurso { get; set; }

        public string Curso { get; set; }

        public int IdCarrera { get; set; }

        public string Carrera { get; set; }

        public List<Pago> Pagos { get; set; }
    }
}