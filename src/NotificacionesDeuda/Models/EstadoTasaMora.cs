using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Models
{
    public enum EstadoTasaMora
    {
        /// <summary>
        /// La tasa está activa.
        /// </summary>
        Activa = 1,

        /// <summary>
        /// La tasa está dada de baja.
        /// </summary>
        Baja = 0
    }
}
