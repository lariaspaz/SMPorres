IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsTotalPagos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ConsTotalPagos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsAlumnosMorosos]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ConsAlumnosMorosos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuariosItemsMenu_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]'))
ALTER TABLE [dbo].[UsuariosItemsMenu] DROP CONSTRAINT [FK_UsuariosItemsMenu_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuariosItemsMenu_ItemsMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]'))
ALTER TABLE [dbo].[UsuariosItemsMenu] DROP CONSTRAINT [FK_UsuariosItemsMenu_ItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RendicionesBSE_CabecerasArchivos]') AND parent_object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]'))
ALTER TABLE [dbo].[RendicionesBSE] DROP CONSTRAINT [FK_RendicionesBSE_CabecerasArchivos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Usuarios1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] DROP CONSTRAINT [FK_PlanesPago_Usuarios1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] DROP CONSTRAINT [FK_PlanesPago_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Cursos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] DROP CONSTRAINT [FK_PlanesPago_Cursos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] DROP CONSTRAINT [FK_PlanesPago_Alumnos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagosWeb_CursosAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagosWeb]'))
ALTER TABLE [dbo].[PagosWeb] DROP CONSTRAINT [FK_PagosWeb_CursosAlumnos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_PlanesPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_PlanesPago]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_Pagos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_MediosPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_MediosPago]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_BecasAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_BecasAlumnos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Localidades_Departamentos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Localidades]'))
ALTER TABLE [dbo].[Localidades] DROP CONSTRAINT [FK_Localidades_Departamentos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposUsuarios_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]'))
ALTER TABLE [dbo].[GruposUsuarios] DROP CONSTRAINT [FK_GruposUsuarios_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposUsuarios_Grupos]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]'))
ALTER TABLE [dbo].[GruposUsuarios] DROP CONSTRAINT [FK_GruposUsuarios_Grupos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposItemsMenus_ItemsMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]'))
ALTER TABLE [dbo].[GruposItemsMenu] DROP CONSTRAINT [FK_GruposItemsMenus_ItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposItemsMenu_Grupos]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]'))
ALTER TABLE [dbo].[GruposItemsMenu] DROP CONSTRAINT [FK_GruposItemsMenu_Grupos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Provincias]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Provincias]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Localidades]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Localidades]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Departamentos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Departamentos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Barrios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Barrios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Departamentos_Provincias]') AND parent_object_id = OBJECT_ID(N'[dbo].[Departamentos]'))
ALTER TABLE [dbo].[Departamentos] DROP CONSTRAINT [FK_Departamentos_Provincias]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnosWeb_AlumnosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]'))
ALTER TABLE [dbo].[CursosAlumnosWeb] DROP CONSTRAINT [FK_CursosAlumnosWeb_AlumnosWeb]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnos_Cursos]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]'))
ALTER TABLE [dbo].[CursosAlumnos] DROP CONSTRAINT [FK_CursosAlumnos_Cursos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]'))
ALTER TABLE [dbo].[CursosAlumnos] DROP CONSTRAINT [FK_CursosAlumnos_Alumnos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Cursos_Carreras]') AND parent_object_id = OBJECT_ID(N'[dbo].[Cursos]'))
ALTER TABLE [dbo].[Cursos] DROP CONSTRAINT [FK_Cursos_Carreras]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CabecerasArchivos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]'))
ALTER TABLE [dbo].[CabecerasArchivos] DROP CONSTRAINT [FK_CabecerasArchivos_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos] DROP CONSTRAINT [FK_BecasAlumnos_Alumnos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Barrios_Localidades]') AND parent_object_id = OBJECT_ID(N'[dbo].[Barrios]'))
ALTER TABLE [dbo].[Barrios] DROP CONSTRAINT [FK_Barrios_Localidades]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AlumnosWeb_RolesUsuariosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[AlumnosWeb]'))
ALTER TABLE [dbo].[AlumnosWeb] DROP CONSTRAINT [FK_AlumnosWeb_RolesUsuariosWeb]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alumnos_TiposDocumento]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alumnos]'))
ALTER TABLE [dbo].[Alumnos] DROP CONSTRAINT [FK_Alumnos_TiposDocumento]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alumnos_Domicilios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alumnos]'))
ALTER TABLE [dbo].[Alumnos] DROP CONSTRAINT [FK_Alumnos_Domicilios]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND name = N'IX_FK_UsuariosItemsMenu_Usuarios')
DROP INDEX [IX_FK_UsuariosItemsMenu_Usuarios] ON [dbo].[UsuariosItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND name = N'IX_FK_UsuariosItemsMenu_ItemsMenu')
DROP INDEX [IX_FK_UsuariosItemsMenu_ItemsMenu] ON [dbo].[UsuariosItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MediosPago]') AND name = N'IX_MediosPago')
DROP INDEX [IX_MediosPago] ON [dbo].[MediosPago]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Localidades]') AND name = N'IX_Localidades_Nombre')
DROP INDEX [IX_Localidades_Nombre] ON [dbo].[Localidades]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND name = N'IX_FK_GruposUsuarios_Usuarios')
DROP INDEX [IX_FK_GruposUsuarios_Usuarios] ON [dbo].[GruposUsuarios]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND name = N'IX_FK_GruposUsuarios_Grupos')
DROP INDEX [IX_FK_GruposUsuarios_Grupos] ON [dbo].[GruposUsuarios]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND name = N'IX_FK_GruposItemsMenu_ItemsMenu')
DROP INDEX [IX_FK_GruposItemsMenu_ItemsMenu] ON [dbo].[GruposItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND name = N'IX_FK_GruposItemsMenu_Grupos')
DROP INDEX [IX_FK_GruposItemsMenu_Grupos] ON [dbo].[GruposItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Departamentos]') AND name = N'IX_Departamentos_Nombre')
DROP INDEX [IX_Departamentos_Nombre] ON [dbo].[Departamentos]
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Barrios]') AND name = N'IX_Barrios_Nombre')
DROP INDEX [IX_Barrios_Nombre] ON [dbo].[Barrios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND type in (N'U'))
DROP TABLE [dbo].[UsuariosItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
DROP TABLE [dbo].[Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposDocumento]') AND type in (N'U'))
DROP TABLE [dbo].[TiposDocumento]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesUsuariosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[RolesUsuariosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]') AND type in (N'U'))
DROP TABLE [dbo].[RendicionesBSE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provincias]') AND type in (N'U'))
DROP TABLE [dbo].[Provincias]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlanesPago]') AND type in (N'U'))
DROP TABLE [dbo].[PlanesPago]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PagosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[PagosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') AND type in (N'U'))
DROP TABLE [dbo].[Pagos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MediosPago]') AND type in (N'U'))
DROP TABLE [dbo].[MediosPago]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Localidades]') AND type in (N'U'))
DROP TABLE [dbo].[Localidades]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemsMenu]') AND type in (N'U'))
DROP TABLE [dbo].[ItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND type in (N'U'))
DROP TABLE [dbo].[GruposUsuarios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND type in (N'U'))
DROP TABLE [dbo].[GruposItemsMenu]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Grupos]') AND type in (N'U'))
DROP TABLE [dbo].[Grupos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Domicilios]') AND type in (N'U'))
DROP TABLE [dbo].[Domicilios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departamentos]') AND type in (N'U'))
DROP TABLE [dbo].[Departamentos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[CursosAlumnosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]') AND type in (N'U'))
DROP TABLE [dbo].[CursosAlumnos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cursos]') AND type in (N'U'))
DROP TABLE [dbo].[Cursos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuotas]') AND type in (N'U'))
DROP TABLE [dbo].[Cuotas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConfiguracionWeb]') AND type in (N'U'))
DROP TABLE [dbo].[ConfiguracionWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Configuracion]') AND type in (N'U'))
DROP TABLE [dbo].[Configuracion]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Carreras]') AND type in (N'U'))
DROP TABLE [dbo].[Carreras]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]') AND type in (N'U'))
DROP TABLE [dbo].[CabecerasArchivos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]') AND type in (N'U'))
DROP TABLE [dbo].[BecasAlumnos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Barrios]') AND type in (N'U'))
DROP TABLE [dbo].[Barrios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlumnosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[AlumnosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alumnos]') AND type in (N'U'))
DROP TABLE [dbo].[Alumnos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Alumnos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Alumnos](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Apellido] [varchar](255) NOT NULL,
	[IdTipoDocumento] [int] NOT NULL,
	[NroDocumento] [numeric](18, 0) NOT NULL,
	[FechaNacimiento] [smalldatetime] NOT NULL,
	[EMail] [varchar](255) NOT NULL,
	[Direccion] [varchar](255) NOT NULL,
	[IdDomicilio] [int] NULL,
	[Estado] [tinyint] NOT NULL,
	[Sexo] [char](1) NOT NULL,
	[Contraseña] [varchar](255) NULL,
 CONSTRAINT [PK_Alumnos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlumnosWeb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AlumnosWeb](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[TipoDocumento] [varchar](50) NOT NULL,
	[NroDocumento] [numeric](18, 0) NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[Contraseña] [varchar](255) NULL,
	[IdRolUsuarioWeb] [int] NOT NULL CONSTRAINT [DF_AlumnosWeb_IdRolUsuarioWeb]  DEFAULT ((1)),
 CONSTRAINT [PK_AlumnoWeb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Barrios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Barrios](
	[Id] [int] NOT NULL,
	[IdLocalidad] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Barrios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BecasAlumnos](
	[Id] [int] NOT NULL,
	[IdAlumno] [int] NOT NULL,
	[PorcentajeBeca] [smallint] NULL,
 CONSTRAINT [PK_BecasAlumnos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CabecerasArchivos](
	[Id] [int] NOT NULL,
	[IdTipoArchivo] [int] NOT NULL,
	[NombreArchivo] [varchar](50) NOT NULL,
	[Hash] [varchar](50) NOT NULL,
	[Fecha] [smalldatetime] NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_CabecerasArchivos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Carreras]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Carreras](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Duracion] [smallint] NOT NULL,
	[Estado] [smallint] NOT NULL,
	[FechaEstado] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Carreras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Configuracion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Configuracion](
	[Id] [int] NOT NULL,
	[DescuentoPagoTermino] [float] NOT NULL,
	[InteresPorMora] [float] NOT NULL,
	[CicloLectivo] [smallint] NOT NULL DEFAULT (datepart(year,getdate())),
	[EndpointAddress] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConfiguracionWeb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ConfiguracionWeb](
	[Id] [int] NOT NULL,
	[InteresPorMora] [float] NOT NULL,
 CONSTRAINT [PK_Configuracion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuotas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cuotas](
	[Id] [int] NOT NULL,
	[Cuota] [smallint] NOT NULL,
	[VtoCuota] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Cuotas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cursos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cursos](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdCarrera] [int] NOT NULL,
	[ImporteCuota] [numeric](18, 2) NOT NULL DEFAULT ((0)),
	[ImporteMatricula] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Cursos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CursosAlumnos](
	[Id] [int] NOT NULL,
	[IdCurso] [int] NOT NULL,
	[IdAlumno] [int] NOT NULL,
	[CicloLectivo] [smallint] NOT NULL DEFAULT (datepart(year,getdate())),
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CursosAlumnosWeb](
	[Id] [int] NOT NULL,
	[IdAlumnoWeb] [int] NOT NULL,
	[IdCurso] [int] NOT NULL,
	[Curso] [varchar](50) NOT NULL,
	[IdCarrera] [int] NOT NULL,
	[Carrera] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CursosAlumnosWeb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departamentos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Departamentos](
	[Id] [int] NOT NULL,
	[IdProvincia] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Departamentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Domicilios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Domicilios](
	[Id] [int] NOT NULL,
	[IdBarrio] [int] NOT NULL,
	[IdLocalidad] [int] NOT NULL,
	[IdDepartamento] [int] NOT NULL,
	[IdProvincia] [int] NOT NULL,
 CONSTRAINT [PK_Domicilios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Grupos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Grupos](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](30) NOT NULL,
	[Estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_Grupos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GruposItemsMenu](
	[Id] [int] NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[IdItemMenu] [int] NOT NULL,
 CONSTRAINT [PK_GruposItemsMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GruposUsuarios](
	[Id] [int] NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_GruposUsuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemsMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ItemsMenu](
	[Id] [int] NOT NULL,
	[IdPadre] [int] NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ItemsMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Localidades]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Localidades](
	[Id] [int] NOT NULL,
	[IdDepartamento] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Localidades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MediosPago]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MediosPago](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MediosPago] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Pagos](
	[Id] [int] NOT NULL,
	[IdPlanPago] [int] NOT NULL,
	[Fecha] [smalldatetime] NULL,
	[NroCuota] [smallint] NOT NULL,
	[ImportePagado] [numeric](18, 2) NULL,
	[ImporteCuota] [numeric](18, 2) NOT NULL,
	[PorcDescPagoTermino] [float] NULL,
	[ImportePagoTermino] [numeric](18, 2) NULL,
	[PorcBeca] [float] NULL,
	[IdBecaAlumno] [int] NULL,
	[ImporteRecargo] [numeric](18, 2) NULL,
	[IdMedioPago] [int] NULL,
	[IdArchivo] [int] NULL,
	[EsContrasiento] [smallint] NULL,
	[IdPagoAsiento] [int] NULL,
	[IdUsuario] [int] NULL,
	[FechaGrabacion] [datetime] NULL,
	[PorcRecargo] [float] NULL,
	[ImporteBeca] [numeric](18, 2) NULL,
	[FechaVto] [datetime] NULL,
	[Descripcion] [varchar](255) NULL,
 CONSTRAINT [PK_Pagos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PagosWeb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PagosWeb](
	[Id] [int] NOT NULL,
	[IdCursoAlumno] [int] NOT NULL,
	[IdPlanPago] [int] NOT NULL,
	[NroCuota] [smallint] NOT NULL,
	[FechaVto] [smalldatetime] NOT NULL,
	[ImporteCuota] [numeric](18, 2) NOT NULL,
	[Fecha] [smalldatetime] NULL,
	[ImportePagado] [numeric](18, 2) NULL,
	[ImporteRecargo] [numeric](18, 2) NULL,
	[ImporteBeca] [numeric](18, 2) NULL,
	[ImportePagoTermino] [numeric](18, 2) NULL,
	[PorcentajeBeca] [smallint] NULL,
 CONSTRAINT [PK_PagosWeb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlanesPago]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PlanesPago](
	[Id] [int] NOT NULL,
	[IdAlumno] [int] NOT NULL,
	[IdCurso] [int] NOT NULL,
	[NroCuota] [smallint] NOT NULL,
	[CantidadCuotas] [smallint] NOT NULL,
	[ImporteCuota] [numeric](18, 2) NOT NULL,
	[PorcentajeBeca] [smallint] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaGrabacion] [datetime] NOT NULL,
	[Estado] [smallint] NOT NULL,
	[IdUsuarioEstado] [int] NOT NULL,
 CONSTRAINT [PK_PlanesPago] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provincias]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Provincias](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Provincias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RendicionesBSE](
	[Id] [int] NOT NULL,
	[IdCabeceraArchivo] [int] NOT NULL,
	[CodigoSucursal] [int] NOT NULL,
	[NombreSucursal] [varchar](50) NOT NULL,
	[Moneda] [varchar](50) NOT NULL,
	[Comprobante] [varchar](50) NOT NULL,
	[TipoMovimiento] [varchar](50) NOT NULL,
	[Importe] [varchar](50) NOT NULL,
	[FechaProceso] [varchar](50) NOT NULL,
	[CuilUsuario] [varchar](50) NOT NULL,
	[NombreUsuario] [varchar](50) NOT NULL,
	[Hora] [varchar](50) NOT NULL,
	[CodigoBarra] [varchar](50) NOT NULL,
	[GrupoTerminal] [varchar](50) NOT NULL,
	[NroRendicion] [varchar](50) NOT NULL,
	[FechaMovimiento] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RendicionesBSE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesUsuariosWeb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RolesUsuariosWeb](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TiposDocumento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TiposDocumento](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TiposDocumento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Contraseña] [varchar](255) NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Estado] [tinyint] NOT NULL,
	[FechaBaja] [datetime] NULL,
	[NombreCompleto] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsuariosItemsMenu](
	[Id] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdItemMenu] [int] NOT NULL,
 CONSTRAINT [PK_UsuariosItemsMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Barrios]') AND name = N'IX_Barrios_Nombre')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Barrios_Nombre] ON [dbo].[Barrios]
(
	[Nombre] ASC,
	[IdLocalidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Departamentos]') AND name = N'IX_Departamentos_Nombre')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Departamentos_Nombre] ON [dbo].[Departamentos]
(
	[Nombre] ASC,
	[IdProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND name = N'IX_FK_GruposItemsMenu_Grupos')
CREATE NONCLUSTERED INDEX [IX_FK_GruposItemsMenu_Grupos] ON [dbo].[GruposItemsMenu]
(
	[IdGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND name = N'IX_FK_GruposItemsMenu_ItemsMenu')
CREATE NONCLUSTERED INDEX [IX_FK_GruposItemsMenu_ItemsMenu] ON [dbo].[GruposItemsMenu]
(
	[IdItemMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND name = N'IX_FK_GruposUsuarios_Grupos')
CREATE NONCLUSTERED INDEX [IX_FK_GruposUsuarios_Grupos] ON [dbo].[GruposUsuarios]
(
	[IdGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND name = N'IX_FK_GruposUsuarios_Usuarios')
CREATE NONCLUSTERED INDEX [IX_FK_GruposUsuarios_Usuarios] ON [dbo].[GruposUsuarios]
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Localidades]') AND name = N'IX_Localidades_Nombre')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Localidades_Nombre] ON [dbo].[Localidades]
(
	[Nombre] ASC,
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[MediosPago]') AND name = N'IX_MediosPago')
CREATE UNIQUE NONCLUSTERED INDEX [IX_MediosPago] ON [dbo].[MediosPago]
(
	[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND name = N'IX_FK_UsuariosItemsMenu_ItemsMenu')
CREATE NONCLUSTERED INDEX [IX_FK_UsuariosItemsMenu_ItemsMenu] ON [dbo].[UsuariosItemsMenu]
(
	[IdItemMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND name = N'IX_FK_UsuariosItemsMenu_Usuarios')
CREATE NONCLUSTERED INDEX [IX_FK_UsuariosItemsMenu_Usuarios] ON [dbo].[UsuariosItemsMenu]
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alumnos_Domicilios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alumnos]'))
ALTER TABLE [dbo].[Alumnos]  WITH CHECK ADD  CONSTRAINT [FK_Alumnos_Domicilios] FOREIGN KEY([IdDomicilio])
REFERENCES [dbo].[Domicilios] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alumnos_Domicilios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alumnos]'))
ALTER TABLE [dbo].[Alumnos] CHECK CONSTRAINT [FK_Alumnos_Domicilios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alumnos_TiposDocumento]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alumnos]'))
ALTER TABLE [dbo].[Alumnos]  WITH CHECK ADD  CONSTRAINT [FK_Alumnos_TiposDocumento] FOREIGN KEY([IdTipoDocumento])
REFERENCES [dbo].[TiposDocumento] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Alumnos_TiposDocumento]') AND parent_object_id = OBJECT_ID(N'[dbo].[Alumnos]'))
ALTER TABLE [dbo].[Alumnos] CHECK CONSTRAINT [FK_Alumnos_TiposDocumento]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AlumnosWeb_RolesUsuariosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[AlumnosWeb]'))
ALTER TABLE [dbo].[AlumnosWeb]  WITH CHECK ADD  CONSTRAINT [FK_AlumnosWeb_RolesUsuariosWeb] FOREIGN KEY([IdRolUsuarioWeb])
REFERENCES [dbo].[RolesUsuariosWeb] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AlumnosWeb_RolesUsuariosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[AlumnosWeb]'))
ALTER TABLE [dbo].[AlumnosWeb] CHECK CONSTRAINT [FK_AlumnosWeb_RolesUsuariosWeb]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Barrios_Localidades]') AND parent_object_id = OBJECT_ID(N'[dbo].[Barrios]'))
ALTER TABLE [dbo].[Barrios]  WITH CHECK ADD  CONSTRAINT [FK_Barrios_Localidades] FOREIGN KEY([IdLocalidad])
REFERENCES [dbo].[Localidades] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Barrios_Localidades]') AND parent_object_id = OBJECT_ID(N'[dbo].[Barrios]'))
ALTER TABLE [dbo].[Barrios] CHECK CONSTRAINT [FK_Barrios_Localidades]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_BecasAlumnos_Alumnos] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Alumnos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos] CHECK CONSTRAINT [FK_BecasAlumnos_Alumnos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CabecerasArchivos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]'))
ALTER TABLE [dbo].[CabecerasArchivos]  WITH CHECK ADD  CONSTRAINT [FK_CabecerasArchivos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CabecerasArchivos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]'))
ALTER TABLE [dbo].[CabecerasArchivos] CHECK CONSTRAINT [FK_CabecerasArchivos_Usuarios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Cursos_Carreras]') AND parent_object_id = OBJECT_ID(N'[dbo].[Cursos]'))
ALTER TABLE [dbo].[Cursos]  WITH CHECK ADD  CONSTRAINT [FK_Cursos_Carreras] FOREIGN KEY([IdCarrera])
REFERENCES [dbo].[Carreras] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Cursos_Carreras]') AND parent_object_id = OBJECT_ID(N'[dbo].[Cursos]'))
ALTER TABLE [dbo].[Cursos] CHECK CONSTRAINT [FK_Cursos_Carreras]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]'))
ALTER TABLE [dbo].[CursosAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_CursosAlumnos_Alumnos] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Alumnos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]'))
ALTER TABLE [dbo].[CursosAlumnos] CHECK CONSTRAINT [FK_CursosAlumnos_Alumnos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnos_Cursos]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]'))
ALTER TABLE [dbo].[CursosAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_CursosAlumnos_Cursos] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Cursos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnos_Cursos]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnos]'))
ALTER TABLE [dbo].[CursosAlumnos] CHECK CONSTRAINT [FK_CursosAlumnos_Cursos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnosWeb_AlumnosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]'))
ALTER TABLE [dbo].[CursosAlumnosWeb]  WITH CHECK ADD  CONSTRAINT [FK_CursosAlumnosWeb_AlumnosWeb] FOREIGN KEY([IdAlumnoWeb])
REFERENCES [dbo].[AlumnosWeb] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnosWeb_AlumnosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]'))
ALTER TABLE [dbo].[CursosAlumnosWeb] CHECK CONSTRAINT [FK_CursosAlumnosWeb_AlumnosWeb]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Departamentos_Provincias]') AND parent_object_id = OBJECT_ID(N'[dbo].[Departamentos]'))
ALTER TABLE [dbo].[Departamentos]  WITH CHECK ADD  CONSTRAINT [FK_Departamentos_Provincias] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincias] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Departamentos_Provincias]') AND parent_object_id = OBJECT_ID(N'[dbo].[Departamentos]'))
ALTER TABLE [dbo].[Departamentos] CHECK CONSTRAINT [FK_Departamentos_Provincias]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Barrios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios]  WITH CHECK ADD  CONSTRAINT [FK_Domicilios_Barrios] FOREIGN KEY([IdBarrio])
REFERENCES [dbo].[Barrios] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Barrios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] CHECK CONSTRAINT [FK_Domicilios_Barrios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Departamentos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios]  WITH CHECK ADD  CONSTRAINT [FK_Domicilios_Departamentos] FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamentos] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Departamentos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] CHECK CONSTRAINT [FK_Domicilios_Departamentos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Localidades]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios]  WITH CHECK ADD  CONSTRAINT [FK_Domicilios_Localidades] FOREIGN KEY([IdLocalidad])
REFERENCES [dbo].[Localidades] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Localidades]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] CHECK CONSTRAINT [FK_Domicilios_Localidades]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Provincias]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios]  WITH CHECK ADD  CONSTRAINT [FK_Domicilios_Provincias] FOREIGN KEY([IdProvincia])
REFERENCES [dbo].[Provincias] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Domicilios_Provincias]') AND parent_object_id = OBJECT_ID(N'[dbo].[Domicilios]'))
ALTER TABLE [dbo].[Domicilios] CHECK CONSTRAINT [FK_Domicilios_Provincias]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposItemsMenu_Grupos]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]'))
ALTER TABLE [dbo].[GruposItemsMenu]  WITH CHECK ADD  CONSTRAINT [FK_GruposItemsMenu_Grupos] FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupos] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposItemsMenu_Grupos]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]'))
ALTER TABLE [dbo].[GruposItemsMenu] CHECK CONSTRAINT [FK_GruposItemsMenu_Grupos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposItemsMenus_ItemsMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]'))
ALTER TABLE [dbo].[GruposItemsMenu]  WITH CHECK ADD  CONSTRAINT [FK_GruposItemsMenus_ItemsMenu] FOREIGN KEY([IdItemMenu])
REFERENCES [dbo].[ItemsMenu] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposItemsMenus_ItemsMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]'))
ALTER TABLE [dbo].[GruposItemsMenu] CHECK CONSTRAINT [FK_GruposItemsMenus_ItemsMenu]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposUsuarios_Grupos]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]'))
ALTER TABLE [dbo].[GruposUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_GruposUsuarios_Grupos] FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupos] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposUsuarios_Grupos]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]'))
ALTER TABLE [dbo].[GruposUsuarios] CHECK CONSTRAINT [FK_GruposUsuarios_Grupos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposUsuarios_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]'))
ALTER TABLE [dbo].[GruposUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_GruposUsuarios_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GruposUsuarios_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]'))
ALTER TABLE [dbo].[GruposUsuarios] CHECK CONSTRAINT [FK_GruposUsuarios_Usuarios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Localidades_Departamentos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Localidades]'))
ALTER TABLE [dbo].[Localidades]  WITH CHECK ADD  CONSTRAINT [FK_Localidades_Departamentos] FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamentos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Localidades_Departamentos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Localidades]'))
ALTER TABLE [dbo].[Localidades] CHECK CONSTRAINT [FK_Localidades_Departamentos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_BecasAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_BecasAlumnos] FOREIGN KEY([IdBecaAlumno])
REFERENCES [dbo].[BecasAlumnos] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_BecasAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_BecasAlumnos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_MediosPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_MediosPago] FOREIGN KEY([IdMedioPago])
REFERENCES [dbo].[MediosPago] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_MediosPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_MediosPago]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Pagos] FOREIGN KEY([IdPagoAsiento])
REFERENCES [dbo].[Pagos] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Pagos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_PlanesPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_PlanesPago] FOREIGN KEY([IdPlanPago])
REFERENCES [dbo].[PlanesPago] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_PlanesPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_PlanesPago]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Usuarios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagosWeb_CursosAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagosWeb]'))
ALTER TABLE [dbo].[PagosWeb]  WITH CHECK ADD  CONSTRAINT [FK_PagosWeb_CursosAlumnos] FOREIGN KEY([IdCursoAlumno])
REFERENCES [dbo].[CursosAlumnosWeb] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagosWeb_CursosAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagosWeb]'))
ALTER TABLE [dbo].[PagosWeb] CHECK CONSTRAINT [FK_PagosWeb_CursosAlumnos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago]  WITH CHECK ADD  CONSTRAINT [FK_PlanesPago_Alumnos] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Alumnos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] CHECK CONSTRAINT [FK_PlanesPago_Alumnos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Cursos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago]  WITH CHECK ADD  CONSTRAINT [FK_PlanesPago_Cursos] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Cursos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Cursos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] CHECK CONSTRAINT [FK_PlanesPago_Cursos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago]  WITH CHECK ADD  CONSTRAINT [FK_PlanesPago_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] CHECK CONSTRAINT [FK_PlanesPago_Usuarios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Usuarios1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago]  WITH CHECK ADD  CONSTRAINT [FK_PlanesPago_Usuarios1] FOREIGN KEY([IdUsuarioEstado])
REFERENCES [dbo].[Usuarios] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlanesPago_Usuarios1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlanesPago]'))
ALTER TABLE [dbo].[PlanesPago] CHECK CONSTRAINT [FK_PlanesPago_Usuarios1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RendicionesBSE_CabecerasArchivos]') AND parent_object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]'))
ALTER TABLE [dbo].[RendicionesBSE]  WITH CHECK ADD  CONSTRAINT [FK_RendicionesBSE_CabecerasArchivos] FOREIGN KEY([IdCabeceraArchivo])
REFERENCES [dbo].[CabecerasArchivos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RendicionesBSE_CabecerasArchivos]') AND parent_object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]'))
ALTER TABLE [dbo].[RendicionesBSE] CHECK CONSTRAINT [FK_RendicionesBSE_CabecerasArchivos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuariosItemsMenu_ItemsMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]'))
ALTER TABLE [dbo].[UsuariosItemsMenu]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosItemsMenu_ItemsMenu] FOREIGN KEY([IdItemMenu])
REFERENCES [dbo].[ItemsMenu] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuariosItemsMenu_ItemsMenu]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]'))
ALTER TABLE [dbo].[UsuariosItemsMenu] CHECK CONSTRAINT [FK_UsuariosItemsMenu_ItemsMenu]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuariosItemsMenu_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]'))
ALTER TABLE [dbo].[UsuariosItemsMenu]  WITH CHECK ADD  CONSTRAINT [FK_UsuariosItemsMenu_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsuariosItemsMenu_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]'))
ALTER TABLE [dbo].[UsuariosItemsMenu] CHECK CONSTRAINT [FK_UsuariosItemsMenu_Usuarios]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsAlumnosMorosos]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ConsAlumnosMorosos] AS' 
END
GO
-- ==========================================================================================
-- Author:		Leonardo Arias Paz
-- Create date: 2019-03-05
-- Description:	Consultar los alumnos al día o morosos en un curso o carrera. Muestra la beca
--				de cada alumno en el resultado.
-- ==========================================================================================
ALTER PROCEDURE [dbo].[ConsAlumnosMorosos]
@Fecha		smalldatetime,
@Tipo		smallint,	-- 1: al día, 2: morosos
@IdCarrera	int,
@IdCurso	int,
@TipoBecado	smallint	-- 0: todos, 1: sin beca, 2: con beca
AS

SET NOCOUNT ON
SET FMTONLY OFF

-- para cada plan obtengo la primera cuota que venza después de la fecha
select p.IdPlanPago, Cuota = MIN(p.NroCuota) 
into #temp
from Pagos p
inner join Cuotas c on c.Cuota = p.NroCuota
where	
	c.VtoCuota >= DATEADD(day, 1 - DAY(@Fecha), @Fecha)	and
	p.EsContrasiento is null
group by p.IdPlanPago

alter table #temp add 
	IdPago		int null,
	FechaPago	smalldatetime null,
	VtoCuota	smalldatetime

-- obtengo la fecha de pago de las cuotas
update #temp
set
	IdPago		= p.Id,
	FechaPago	= p.Fecha
from #temp t
inner join Pagos p on p.IdPlanPago = t.IdPlanPago and p.NroCuota = t.Cuota

update #temp
set
	VtoCuota = c.VtoCuota
from #temp t
inner join Cuotas c on t.Cuota = c.Cuota

select	IdCurso = c.Id, c.Nombre AS Curso, IdCarrera = c1.Id, c1.Nombre AS Carrera, TipoDocumento = (select Descripcion from TiposDocumento td where td.Id = a.IdTipoDocumento), 
		a.NroDocumento, a.Nombre, a.Apellido, t.VtoCuota, t.FechaPago, t.Cuota, p.ImporteCuota, p.ImportePagado, Beca = ISNULL(ba.PorcBeca, p.PorcBeca)
into #salida
from #temp t
inner join PlanesPago pp on t.IdPlanPago = pp.Id
inner join Pagos p on t.IdPago = p.Id
inner join Alumnos a on pp.IdAlumno = a.Id
inner join Cursos c on pp.IdCurso = c.Id
inner join Carreras c1 on c.IdCarrera = c1.Id
left join BecasAlumnos ba on ba.Id = p.IdBecaAlumno
where a.Estado	= 1
order by Carrera, Curso

if @IdCarrera > 0
	delete from #salida where IdCarrera <> @IdCarrera

if @IdCurso > 0
	delete from #salida where IdCurso <> @IdCurso

-- 0: todos, 1: sin beca, 2: con beca
if @TipoBecado	= 1	
	delete from #salida where ISNULL(beca, 0) > 0
else 
	if @TipoBecado	= 2
		delete from #salida where ISNULL(beca, 0) = 0	

-- 1: al día, 2: morosos
if @Tipo = 1
	select * from #salida where FechaPago is not null
else
	select * from #salida where FechaPago is null



GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ConsTotalPagos]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ConsTotalPagos] AS' 
END
GO
-- ==========================================================================================
-- Author:		Leonardo Arias Paz
-- Create date: 2019-03-05
-- Description:	Consulta los montos y cantidades de cuotas totales cobrados en un período a 
--				un curso o carrera.
-- ==========================================================================================
ALTER PROCEDURE [dbo].[ConsTotalPagos]
@Desde		smalldatetime,
@Hasta		smalldatetime,
@IdCarrera	int,
@IdCurso	int
AS

SET NOCOUNT ON;
SET FMTONLY OFF

create table #temp(
	IdCurso		int				not null,
	IdCarrera	int				not null,
	Cantidad	int				not null,
	Total		numeric(18,2)	not null
)

insert into #temp (
		IdCurso,		IdCarrera,		Cantidad,				Total
)
select	pp.IdCurso,		c.IdCarrera,	Cantidad = COUNT(1),	Total = SUM(p.ImportePagado) 
from Pagos p
inner join PlanesPago pp on pp.Id = p.IdPlanPago
inner join Cursos c on c.Id = pp.IdCurso
where p.fecha between @Desde and @Hasta
group by pp.IdCurso,	c.IdCarrera

if @IdCarrera > 0 delete from #temp where IdCarrera <> @IdCarrera

if @IdCurso > 0 delete from #temp where IdCurso <> @IdCurso

select Carrera = c1.Nombre, Curso = c.Nombre, t.IdCarrera, t.IdCurso, t.Cantidad, t.Total
from #temp t
inner join Cursos c on t.IdCurso = c.Id
inner join Carreras c1 on t.IdCarrera = c1.Id
order by Carrera, Curso

GO
