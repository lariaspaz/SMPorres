IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos] DROP CONSTRAINT [FK_BecasAlumnos_Pagos]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos] DROP CONSTRAINT [FK_BecasAlumnos_Alumnos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]') AND type in (N'U'))
DROP TABLE [dbo].[BecasAlumnos]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BecasAlumnos](
	[Id] [int] NOT NULL,
	[IdPago] [int] NOT NULL,
	[IdAlumno] [int] NOT NULL,
	[PorcBeca] [float] NOT NULL,
 CONSTRAINT [PK_BecasAlumnos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_BecasAlumnos_Alumnos] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Alumnos] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Alumnos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos] CHECK CONSTRAINT [FK_BecasAlumnos_Alumnos]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos]  WITH CHECK ADD  CONSTRAINT [FK_BecasAlumnos_Pagos] FOREIGN KEY([IdPago])
REFERENCES [dbo].[Pagos] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BecasAlumnos_Pagos]') AND parent_object_id = OBJECT_ID(N'[dbo].[BecasAlumnos]'))
ALTER TABLE [dbo].[BecasAlumnos] CHECK CONSTRAINT [FK_BecasAlumnos_Pagos]
GO
