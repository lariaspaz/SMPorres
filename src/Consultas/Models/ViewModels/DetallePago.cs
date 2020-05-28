using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models.ViewModels
{
    public class DetallePago
    {
        public string Curso { get; set; }

        public PagoWeb PróximaCuota { get; set; }

        public IEnumerable<PagoWeb> Pagos { get; set; }

        public int Id { get; set; }
    }
}