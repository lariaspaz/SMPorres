IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TasasMoraWeb]') AND type in (N'U'))
DROP TABLE [dbo].[TasasMoraWeb]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TasasMoraWeb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TasasMoraWeb](
	[Id] [int] NOT NULL,
	[Tasa] [float] NOT NULL,
	[Desde] [smalldatetime] NOT NULL,
	[Hasta] [smalldatetime] NOT NULL,
	[Estado] [smallint] NOT NULL,
 CONSTRAINT [PK_TasasMoraWeb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
