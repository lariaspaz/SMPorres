using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public enum EstadoPlanPago : short
    {
        /// <summary>
        /// El plan de pago está vigente, las cuotas se pueden seguir abonando normalmente.
        /// </summary>
        Vigente = 1,

        /// <summary>
        /// El plan de pago ha sido abonado en su totalidad, no hay cuotas pendientes de abonar.
        /// </summary>
        Cancelado = 2,

        /// <summary>
        /// El plan de pago se ha dado de baja. No se pueden seguir abonando las cuotas.
        /// </summary>
        Baja = 3
    }
}
