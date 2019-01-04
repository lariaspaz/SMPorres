/****** Object:  Table [dbo].[Usuarios]    Script Date: 02/01/2019 11:39:59 a.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
DROP TABLE [dbo].[Usuarios]
GO
/****** Object:  Table [dbo].[Carreras]    Script Date: 02/01/2019 11:39:59 a.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Carreras]') AND type in (N'U'))
DROP TABLE [dbo].[Carreras]
GO
/****** Object:  Table [dbo].[Carreras]    Script Date: 02/01/2019 11:39:59 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Carreras]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Carreras](
	[Id] [decimal](18, 0) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Duracion] [smallint] NOT NULL,
	[Importe1Vto] [decimal](18, 2) NOT NULL,
	[Importe2Vto] [decimal](18, 2) NOT NULL,
	[Importe3Vto] [decimal](18, 3) NOT NULL,
	[Estado] [smallint] NOT NULL,
	[FechaEstado] [datetime] NOT NULL,
 CONSTRAINT [PK_Carreras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Carreras] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 02/01/2019 11:39:59 a.m. ******/
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
ALTER AUTHORIZATION ON [dbo].[Usuarios] TO  SCHEMA OWNER 
GO
