//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMPorres.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RendicionBSE
    {
        public int Id { get; set; }
        public int IdCabeceraArchivo { get; set; }
        public int CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Moneda { get; set; }
        public string Comprobante { get; set; }
        public string TipoMovimiento { get; set; }
        public string Importe { get; set; }
        public string FechaProceso { get; set; }
        public string CuilUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Hora { get; set; }
        public string CodigoBarra { get; set; }
        public string GrupoTerminal { get; set; }
        public string NroRendicion { get; set; }
        public string FechaMovimiento { get; set; }
    
        public virtual CabeceraArchivo CabeceraArchivo { get; set; }
    }
}
