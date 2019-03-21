using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultas.Models.WebServices
{
    public class Pago
    {
        public int Id { get; set; }

        public int IdPlanPago { get; set; }

        public int NroCuota { get; set; }

        public DateTime FechaVto { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal ImporteCuota { get; set; }

        public decimal? ImportePagado { get; set; }

        public decimal? ImporteRecargo { get; set; }

        public decimal? ImporteBeca { get; set; }
    }
}