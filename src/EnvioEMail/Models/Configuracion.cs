//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnvioEMail.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Configuracion
    {
        public int Id { get; set; }
        public double DescuentoPagoTermino { get; set; }
        public double InteresPorMora { get; set; }
        public short CicloLectivo { get; set; }
        public string EndpointAddress { get; set; }
        public Nullable<short> DiasVtoPagoTermino { get; set; }
    }
}
