﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SMPorresEntities : DbContext
    {
        public SMPorresEntities()
            : base("name=SMPorresEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AlumnoWeb> AlumnosWeb { get; set; }
        public virtual DbSet<CursoAlumnoWeb> CursosAlumnosWeb { get; set; }
        public virtual DbSet<PagoWeb> PagosWeb { get; set; }
        public virtual DbSet<RolUsuarioWeb> RolesUsuariosWeb { get; set; }
        public virtual DbSet<ConfiguracionWeb> ConfiguracionesWeb { get; set; }
    }
}
