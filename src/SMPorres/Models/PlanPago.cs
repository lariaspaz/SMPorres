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
    
    public partial class PlanPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanPago()
        {
            this.Pagos = new HashSet<Pago>();
        }
    
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdCurso { get; set; }
        public short NroCuota { get; set; }
        public short CantidadCuotas { get; set; }
        public decimal ImporteCuota { get; set; }
        public short PorcentajeBeca { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime FechaGrabacion { get; set; }
        public short Estado { get; set; }
        public int IdUsuarioEstado { get; set; }
        public Nullable<int> MinCuotas { get; set; }
        public Nullable<int> MaxCuotas { get; set; }
    
        public virtual Alumno Alumno { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario UsuarioEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
