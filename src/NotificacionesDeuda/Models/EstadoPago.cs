﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Models
{
    public enum EstadoPago
    {
        /// <summary>
        /// El pago está activo.
        /// </summary>
        Impago = 1,

        /// <summary>
        /// El pago está dado de baja.
        /// </summary>
        Baja = 2,

        /// <summary>
        /// El pago está cancelado.
        /// </summary>
        Pagado = 3

        
    }
}
