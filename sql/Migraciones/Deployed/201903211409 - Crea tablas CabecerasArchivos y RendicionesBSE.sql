IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RendicionesBSE_CabecerasArchivos]') AND parent_object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]'))
ALTER TABLE [dbo].[RendicionesBSE] DROP CONSTRAINT [FK_RendicionesBSE_CabecerasArchivos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CabecerasArchivos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]'))
ALTER TABLE [dbo].[CabecerasArchivos] DROP CONSTRAINT [FK_CabecerasArchivos_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]') AND type in (N'U'))
DROP TABLE [dbo].[RendicionesBSE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]') AND type in (N'U'))
DROP TABLE [dbo].[CabecerasArchivos]
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CabecerasArchivos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]'))
ALTER TABLE [dbo].[CabecerasArchivos]  WITH CHECK ADD  CONSTRAINT [FK_CabecerasArchivos_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CabecerasArchivos_Usuarios]') AND parent_object_id = OBJECT_ID(N'[dbo].[CabecerasArchivos]'))
ALTER TABLE [dbo].[CabecerasArchivos] CHECK CONSTRAINT [FK_CabecerasArchivos_Usuarios]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RendicionesBSE_CabecerasArchivos]') AND parent_object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]'))
ALTER TABLE [dbo].[RendicionesBSE]  WITH CHECK ADD  CONSTRAINT [FK_RendicionesBSE_CabecerasArchivos] FOREIGN KEY([IdCabeceraArchivo])
REFERENCES [dbo].[CabecerasArchivos] ([Id])
ON UPDATE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RendicionesBSE_CabecerasArchivos]') AND parent_object_id = OBJECT_ID(N'[dbo].[RendicionesBSE]'))
ALTER TABLE [dbo].[RendicionesBSE] CHECK CONSTRAINT [FK_RendicionesBSE_CabecerasArchivos]
GO
