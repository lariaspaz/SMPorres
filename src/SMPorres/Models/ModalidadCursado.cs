using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Models
{
    public enum ModalidadCursado
    {
        /// <summary>
        /// El cursado es anual, se lleva a cabo durante todo el año.
        /// </summary>
        Anual = 1,

        /// <summary>
        /// El cursado se realiza durante el primer cuatrimestre, desde marzo a junio.
        /// </summary>
        PrimerCuatrimestre = 2,

        /// <summary>
        /// El cursado se realiza durante el segundo cuatrimestre, desde julio a diciembre.
        /// </summary>
        SegundoCuatrimestre = 3,

        /// <summary>
        /// No se realiza cursado durante el ciclo lectivo.
        /// </summary>
        SinCursado = 4
    }
}
