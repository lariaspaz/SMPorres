IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagosWeb_CursosAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagosWeb]'))
ALTER TABLE [dbo].[PagosWeb] DROP CONSTRAINT [FK_PagosWeb_CursosAlumnos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnosWeb_AlumnosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]'))
ALTER TABLE [dbo].[CursosAlumnosWeb] DROP CONSTRAINT [FK_CursosAlumnosWeb_AlumnosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PagosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[PagosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[CursosAlumnosWeb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlumnosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[AlumnosWeb]
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
	[Estado] tinyint NOT NULL
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
	ImporteRecargo numeric(18, 2) NULL,
	ImporteBeca	numeric(18, 2) NULL
 CONSTRAINT [PK_PagosWeb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnosWeb_AlumnosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]'))
ALTER TABLE [dbo].[CursosAlumnosWeb]  WITH CHECK ADD  CONSTRAINT [FK_CursosAlumnosWeb_AlumnosWeb] FOREIGN KEY([IdAlumnoWeb])
REFERENCES [dbo].[AlumnosWeb] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CursosAlumnosWeb_AlumnosWeb]') AND parent_object_id = OBJECT_ID(N'[dbo].[CursosAlumnosWeb]'))
ALTER TABLE [dbo].[CursosAlumnosWeb] CHECK CONSTRAINT [FK_CursosAlumnosWeb_AlumnosWeb]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagosWeb_CursosAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagosWeb]'))
ALTER TABLE [dbo].[PagosWeb]  WITH CHECK ADD  CONSTRAINT [FK_PagosWeb_CursosAlumnos] FOREIGN KEY([IdCursoAlumno])
REFERENCES [dbo].[CursosAlumnosWeb] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PagosWeb_CursosAlumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[PagosWeb]'))
ALTER TABLE [dbo].[PagosWeb] CHECK CONSTRAINT [FK_PagosWeb_CursosAlumnos]
GO
