drop TABLE [dbo].[Pagos]
GO

/****** Object:  Table [dbo].[Pagos]    Script Date: 11/02/2019 10:00:25 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pagos](
	[IdPago] [int] NOT NULL,
	[IdPlanPago] [int] NOT NULL,
	[Fecha] [smalldatetime] NULL,
	[NroCuota] [smallint] NOT NULL,
	[ImportePagado] [numeric](18, 2) NULL,
	[ImporteCuota] [numeric](18, 2) NOT NULL,
	[PorcDescPagoTermino] [smallint] NULL,
	[ImportePagoTermino] [numeric](18, 2) NULL,
	[PorcentajeBeca] [smallint] NULL,
	[IdBecaAlumno] [int] NULL,
	[Recargo] [numeric](18, 2) NULL,
	[IdMedioPago] [int] NULL,
	[IdArchivo] [int] NULL,
	[EsContrasiento] [smallint] NULL,
	[IdPagoAsiento] [int] NULL,
	[IdUsuario] [int] NULL,
	[FechaGrabacion] [datetime] NULL,
 CONSTRAINT [PK_Pagos] PRIMARY KEY CLUSTERED 
(
	[IdPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Pagos] FOREIGN KEY([IdPagoAsiento])
REFERENCES [dbo].[Pagos] ([IdPago])
GO

ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Pagos]
GO

ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_PlanesPago] FOREIGN KEY([IdPlanPago])
REFERENCES [dbo].[PlanesPago] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_PlanesPago]
GO

ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO

ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Usuarios]
GO


