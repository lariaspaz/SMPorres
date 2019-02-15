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
    
    public partial class Pago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pago()
        {
            this.Asiento = new HashSet<Pago>();
        }
    
        public int IdPago { get; set; }
        public int IdPlanPago { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public short NroCuota { get; set; }
        public Nullable<decimal> ImportePagado { get; set; }
        public decimal ImporteCuota { get; set; }
        public Nullable<short> PorcDescPagoTermino { get; set; }
        public Nullable<decimal> ImportePagoTermino { get; set; }
        public Nullable<short> PorcentajeBeca { get; set; }
        public Nullable<int> IdBecaAlumno { get; set; }
        public Nullable<decimal> Recargo { get; set; }
        public Nullable<int> IdMedioPago { get; set; }
        public Nullable<int> IdArchivo { get; set; }
        public Nullable<short> EsContrasiento { get; set; }
        public Nullable<int> IdPagoAsiento { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> FechaGrabacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Asiento { get; set; }
        public virtual Pago Contrasiento { get; set; }
        public virtual PlanPago PlanPago { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
