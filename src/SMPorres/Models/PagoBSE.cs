using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public class PagoBSE : RendicionBSE
    {
        public string Documento { get; set; }

        public string Alumno { get; set; }

        public string Carrera { get; set; }

        public string Curso { get; set; }

        public DateTime FechaVto { get; set; }

        public DateTime FechaPago { get; set; }

        public decimal ImportePagado { get; set; }
    }
}
