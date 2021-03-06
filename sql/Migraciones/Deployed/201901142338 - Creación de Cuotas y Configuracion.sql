IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuotas]') AND type in (N'U'))
DROP TABLE [dbo].[Cuotas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Configuracion]') AND type in (N'U'))
DROP TABLE [dbo].[Configuracion]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Configuracion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Configuracion](
	[Id] [int] NOT NULL,
	[DescuentoPagoTermino] [float] NOT NULL,
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
	[VtoBonificacion] [smalldatetime] NOT NULL,
	[VtoCuota] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Cuotas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
