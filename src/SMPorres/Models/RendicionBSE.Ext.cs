using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public partial class RendicionBSE
    {
        public int IdComprobante
        {
            get
            {
                return Int32.Parse(Comprobante);
            }
        }

        public decimal ImportePagado
        {
            get
            {
                return Decimal.Parse(Importe.Replace(".", ","));
            }
        }

        public DateTime FechaPago
        {
            get
            {
                var año = Int32.Parse(FechaMovimiento.Substring(0, 4));
                var mes = Int32.Parse(FechaMovimiento.Substring(5, 2));
                var día = Int32.Parse(FechaMovimiento.Substring(8, 2));
                return new DateTime(año, mes, día);
            }
        }

        public DateTime FechaVto
        {            
            get
            {
                var año = Int32.Parse(CodigoBarra.Substring(11, 2));
                var días = Int32.Parse(CodigoBarra.Substring(13, 3));
                return new DateTime(2000 + año, 1, 1).AddDays(días - 1);
            }
        }

    }
}
