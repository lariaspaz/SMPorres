using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models
{
    public partial class PagoWeb
    {
        public bool AplicaBeca { get; set; }

        public bool PagaATérmino { get; set; }

        public bool Vencido { get; set; }
    }
}