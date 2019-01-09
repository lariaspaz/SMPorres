/****** Object:  Table [dbo].[CursosAlumnos]    Script Date: 09/01/2019 12:51:15 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CursosAlumnos](
	[Id] [int] NOT NULL,
	[IdCurso] [int] NOT NULL,
	[IdAlumno] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CursosAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_CursosAlumnos_Alumnos] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Alumnos] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CursosAlumnos] CHECK CONSTRAINT [FK_CursosAlumnos_Alumnos]
GO

ALTER TABLE [dbo].[CursosAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_CursosAlumnos_Cursos] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Cursos] ([Id])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CursosAlumnos] CHECK CONSTRAINT [FK_CursosAlumnos_Cursos]
GO


