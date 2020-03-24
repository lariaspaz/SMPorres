using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvioEMail.Models
{
    public class Moroso
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Fecha { get; set; }

        public int CantidadCuotas { get; set; }

        public string Carrera { get; set; }

        public string Importe { get; set; }
    }
}
