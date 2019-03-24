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

        public bool Válido { get; set; }

        public Pago DetallePago
        {
            get
            {
                return Repositories.PagosRepository.ObtenerDetallePago(Id, FechaVto);
            }
        }

        public decimal? ImporteAPagar
        {
            get
            {
                return DetallePago != null ? DetallePago.ImportePagado : null;
            }
        }
    }
}
