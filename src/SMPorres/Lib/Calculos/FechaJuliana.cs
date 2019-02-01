using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Lib.Calculos
{
    public class FechaJuliana
    {
        public static string CalcularFechaJuliana(DateTime fechaVencimiento)
        {
            DateTime diaUno = new DateTime(fechaVencimiento.Year, 1, 1);

            int diferenciaDias = (fechaVencimiento - diaUno).Days + 1;

            string fechaJuliana = fechaVencimiento.ToString("yy") + diferenciaDias.ToString("000");

            return fechaJuliana;
        }

        public static DateTime LeerFechaJuliana(string fechaJuliana)
        {
            int año = 2000 + Convert.ToInt16(fechaJuliana.Substring(0, 2));
            int dias = Convert.ToInt16(fechaJuliana.Substring(2, 3));

            DateTime primerDia = new DateTime(año, 1, 1);

            DateTime fechaVencimiento = primerDia.AddDays(dias - 1);

            return fechaVencimiento;
        }
    }
}
