﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
    
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Barrio> Barrios { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Domicilio> Domicilios { get; set; }
        public virtual DbSet<Localidad> Localidades { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Grupos> Grupos { get; set; }
        public virtual DbSet<GruposUsuario> GruposUsuarios { get; set; }
        public virtual DbSet<TipoDocumento> TiposDocumento { get; set; }
        public virtual DbSet<CursosAlumno> CursosAlumnos { get; set; }
        public virtual DbSet<PlanPago> PlanesPago { get; set; }
        public virtual DbSet<Configuracion> Configuraciones { get; set; }
        public virtual DbSet<Cuota> Cuotas { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<GruposItemsMenu> GruposItemsMenus { get; set; }
        public virtual DbSet<ItemsMenu> ItemsMenus { get; set; }
        public virtual DbSet<UsuariosItemsMenu> UsuariosItemsMenus { get; set; }
        public virtual DbSet<MedioPago> MediosPago { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<BecaAlumno> BecasAlumnos { get; set; }
        public virtual DbSet<RendicionBSE> RendicionesBSE { get; set; }
        public virtual DbSet<CabeceraArchivo> CabecerasArchivos { get; set; }
    
        public virtual ObjectResult<ConsTotalPagos_Result> ConsTotalPagos(Nullable<System.DateTime> desde, Nullable<System.DateTime> hasta, Nullable<int> idCarrera, Nullable<int> idCurso)
        {
            var desdeParameter = desde.HasValue ?
                new ObjectParameter("Desde", desde) :
                new ObjectParameter("Desde", typeof(System.DateTime));
    
            var hastaParameter = hasta.HasValue ?
                new ObjectParameter("Hasta", hasta) :
                new ObjectParameter("Hasta", typeof(System.DateTime));
    
            var idCarreraParameter = idCarrera.HasValue ?
                new ObjectParameter("IdCarrera", idCarrera) :
                new ObjectParameter("IdCarrera", typeof(int));
    
            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConsTotalPagos_Result>("ConsTotalPagos", desdeParameter, hastaParameter, idCarreraParameter, idCursoParameter);
        }
    
        public virtual ObjectResult<ConsAlumnosMorosos_Result> ConsAlumnosMorosos(Nullable<System.DateTime> fecha, Nullable<short> tipo, Nullable<int> idCarrera, Nullable<int> idCurso, Nullable<short> tipoBecado)
        {
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("Tipo", tipo) :
                new ObjectParameter("Tipo", typeof(short));
    
            var idCarreraParameter = idCarrera.HasValue ?
                new ObjectParameter("IdCarrera", idCarrera) :
                new ObjectParameter("IdCarrera", typeof(int));
    
            var idCursoParameter = idCurso.HasValue ?
                new ObjectParameter("IdCurso", idCurso) :
                new ObjectParameter("IdCurso", typeof(int));
    
            var tipoBecadoParameter = tipoBecado.HasValue ?
                new ObjectParameter("TipoBecado", tipoBecado) :
                new ObjectParameter("TipoBecado", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ConsAlumnosMorosos_Result>("ConsAlumnosMorosos", fechaParameter, tipoParameter, idCarreraParameter, idCursoParameter, tipoBecadoParameter);
        }
    }
}
