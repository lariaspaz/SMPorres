//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotificacionesDeuda.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BecaAlumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BecaAlumno()
        {
            this.Pagos = new HashSet<Pago>();
        }
    
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public Nullable<short> PorcentajeBeca { get; set; }
    
        public virtual Alumno Alumnos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
