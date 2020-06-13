using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Models
{
    class AlumnoMoroso
    {
        public int IdPlanPago { get; set; }

        public string Carrera { get; set; }

        public string Curso { get; set; }

        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public decimal NroDocumento { get; set; }

        public string EMail { get; set; }

        public Int32 IdPago { get; set; }

        public Int16 NroCuota { get; set; }

        public decimal? ImporteDeuda { get; set; }

        public int CuotasAdeudadas { get; set; }

    }
}
