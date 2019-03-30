IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesUsuariosWeb]') AND type in (N'U'))
DROP TABLE [dbo].[RolesUsuariosWeb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RolesUsuariosWeb](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

INSERT INTO RolesUsuariosWeb(Id, Nombre) VALUES(1, 'Public')
go

IF EXISTS (select * from sys.default_constraints where name = 'DF_AlumnosWeb_IdRolUsuarioWeb' and parent_object_id = OBJECT_ID('AlumnosWeb'))
ALTER TABLE AlumnosWeb DROP DF_AlumnosWeb_IdRolUsuarioWeb
go

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_AlumnosWeb_RolesUsuariosWeb') AND parent_object_id = OBJECT_ID(N'AlumnosWeb'))
ALTER TABLE [dbo].[AlumnosWeb] DROP [FK_AlumnosWeb_RolesUsuariosWeb] 
GO

IF  EXISTS (select * from sys.columns where name = 'IdRolUsuarioWeb' and object_id = OBJECT_ID('AlumnosWeb'))
ALTER TABLE [dbo].[AlumnosWeb] DROP COLUMN IdRolUsuarioWeb
GO

alter table AlumnosWeb add IdRolUsuarioWeb int NOT NULL CONSTRAINT DF_AlumnosWeb_IdRolUsuarioWeb DEFAULT(1)
go

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_AlumnosWeb_RolesUsuariosWeb') AND parent_object_id = OBJECT_ID(N'AlumnosWeb'))
ALTER TABLE [dbo].[AlumnosWeb]  WITH CHECK ADD  CONSTRAINT [FK_AlumnosWeb_RolesUsuariosWeb] FOREIGN KEY([IdRolUsuarioWeb])
REFERENCES [dbo].[RolesUsuariosWeb] ([Id])
ON UPDATE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_AlumnosWeb_RolesUsuariosWeb') AND parent_object_id = OBJECT_ID(N'AlumnosWeb'))
ALTER TABLE AlumnosWeb CHECK CONSTRAINT [FK_AlumnosWeb_RolesUsuariosWeb]
GO
