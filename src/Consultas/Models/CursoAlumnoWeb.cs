//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Consultas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CursoAlumnoWeb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CursoAlumnoWeb()
        {
            this.PagosWeb = new HashSet<PagoWeb>();
        }
    
        public int Id { get; set; }
        public int IdAlumnoWeb { get; set; }
        public int IdCurso { get; set; }
        public string Curso { get; set; }
        public int IdCarrera { get; set; }
        public string Carrera { get; set; }
    
        public virtual AlumnoWeb AlumnosWeb { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PagoWeb> PagosWeb { get; set; }
    }
}
