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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.GruposUsuarios = new HashSet<GruposUsuario>();
            this.PlanesPagos = new HashSet<PlanesPago>();
            this.PlanesPagosEstado = new HashSet<PlanesPago>();
            this.Pagos = new HashSet<Pago>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public byte Estado { get; set; }
        public Nullable<System.DateTime> FechaBaja { get; set; }
        public string NombreCompleto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GruposUsuario> GruposUsuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanesPago> PlanesPagos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanesPago> PlanesPagosEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
