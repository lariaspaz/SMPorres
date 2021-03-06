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
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_PlanesPago]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos] DROP CONSTRAINT [FK_Pagos_PlanesPago]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlanesPago]') AND type in (N'U'))
DROP TABLE [dbo].[PlanesPago]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') AND type in (N'U'))
DROP TABLE [dbo].[Pagos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pagos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Pagos](
	[IdPago] [int] NOT NULL,
	[IdPlanPago] [int] NOT NULL,
	[Fecha] [smalldatetime] NOT NULL,
	[NroCuota] [smallint] NOT NULL,
	[ImportePagado] [numeric](18, 2) NOT NULL,
	[ImporteCuota] [numeric](18, 2) NOT NULL,
	[PorcDescPagoTermino] [smallint] NOT NULL,
	[ImportePagoTermino] [numeric](18, 2) NOT NULL,
	[PorcentajeBeca] [smallint] NOT NULL,
	[IdBecaAlumno] [int] NULL,
	[Recargo] [numeric](18, 2) NOT NULL,
	[IdMedioPago] [int] NOT NULL,
	[IdArchivo] [int] NULL,
	[EsContrasiento] [smallint] NOT NULL,
	[IdPagoAsiento] [int] NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaGrabacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Pagos] PRIMARY KEY CLUSTERED 
(
	[IdPago] ASC
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pagos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pagos]'))
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Pagos] FOREIGN KEY([IdPagoAsiento])
REFERENCES [dbo].[Pagos] ([IdPago])
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
