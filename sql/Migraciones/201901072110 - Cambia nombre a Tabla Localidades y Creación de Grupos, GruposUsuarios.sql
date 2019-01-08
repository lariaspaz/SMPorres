exec sp_rename 'Localidads', 'Localidades'
go


/****** Object:  Table [dbo].[Grupos]    Script Date: 07/01/2019 10:23:59 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Grupos]') AND type in (N'U'))
DROP TABLE [dbo].[Grupos]
GO
/****** Object:  Table [dbo].[Grupos]    Script Date: 07/01/2019 10:23:59 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Grupos]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Grupos](
	[Id] [int] NOT NULL,
	[Descripcion] [varchar](30) NOT NULL,
	[Estado] [tinyint] NOT NULL,
	CONSTRAINT [PK_Grupos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Grupos] TO  SCHEMA OWNER 
GO





/****** Object:  Table [dbo].[GruposUsuarios]    Script Date: 07/01/2019 23:09:59 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND type in (N'U'))
DROP TABLE [dbo].[GruposUsuarios]
GO
/****** Object:  Table [dbo].[GruposUsuarios]    Script Date: 07/01/2019 23:09:59 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposUsuarios]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GruposUsuarios](
	[Id] [int] NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	CONSTRAINT [PK_GruposUsuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[GruposUsuarios] TO  SCHEMA OWNER 
GO



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdGrupo] in table 'GruposUsuarios'
ALTER TABLE [dbo].[GruposUsuarios]
ADD CONSTRAINT [FK_GruposUsuarios_Grupos]
    FOREIGN KEY ([IdGrupo])
    REFERENCES [dbo].[Grupos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GruposUsuarios_Grupos'
CREATE INDEX [IX_FK_GruposUsuarios_Grupos]
ON [dbo].[GruposUsuarios]
    ([IdGrupo]);
GO

-- Creating foreign key on [IdUsuario] in table 'GruposUsuarios'
ALTER TABLE [dbo].[GruposUsuarios]
ADD CONSTRAINT [FK_GruposUsuarios_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GruposUsuarios_Grupos'
CREATE INDEX [IX_FK_GruposUsuarios_Usuarios]
ON [dbo].[GruposUsuarios]
    ([IdUsuario]);
GO
