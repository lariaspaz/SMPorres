using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models.WebServices
{
    public class TasaMora
    {
        public double Tasa { get; set; }

        public DateTime Desde { get; set; }

        public DateTime Hasta { get; set; }

        public short Estado { get; set; }
    }
}