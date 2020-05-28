drop table #carreras
go
select * into #carreras from Carreras
go

drop table Carreras
go

/****** Object:  Table [dbo].[Carreras]    Script Date: 04/01/2019 07:48:20 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Carreras](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Duracion] [smallint] NOT NULL,
	[Importe] [numeric](18, 2) NOT NULL,
	[Estado] [smallint] NOT NULL,
	[FechaEstado] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Carreras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

insert into carreras select * from #carreras
go

select * from Carreras
