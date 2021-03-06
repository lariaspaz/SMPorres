IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TasasMora]') AND type in (N'U'))
DROP TABLE [dbo].[TasasMora]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TasasMora]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TasasMora](
	[Id] [int] NOT NULL,
	[Tasa] [float] NOT NULL,
	[Desde] [smalldatetime] NOT NULL,
	[Hasta] [smalldatetime] NOT NULL,
	[Estado] [smallint] NOT NULL,
 CONSTRAINT [PK_TasasMora] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

-- Inserta la tasa de mora que ya existe en la Configuracion
insert into TasasMora
select 1, InteresPorMora, '2019/01/01', '2020/12/31', 1 from Configuracion

