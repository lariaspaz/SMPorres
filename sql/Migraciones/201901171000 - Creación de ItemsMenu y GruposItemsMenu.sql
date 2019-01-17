
/****** Object:  Table [dbo].[ItemsMenu]    Script Date: 07/01/2019 10:23:59 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemsMenu]') AND type in (N'U'))
DROP TABLE [dbo].[ItemsMenu]
GO
/****** Object:  Table [dbo].[ItemsMenu]    Script Date: 07/01/2019 10:23:59 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemsMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ItemsMenu](
	[Id] [int] NOT NULL,
	[IdPadre] [int] NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	CONSTRAINT [PK_ItemsMenu] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[ItemsMenu] TO  SCHEMA OWNER 
GO





/****** Object:  Table [dbo].[GruposItemsMenu]    Script Date: 07/01/2019 23:09:59 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND type in (N'U'))
DROP TABLE [dbo].[GruposItemsMenu]
GO
/****** Object:  Table [dbo].[GruposItemsMenu]    Script Date: 07/01/2019 23:09:59 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GruposItemsMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GruposItemsMenu](
	[Id] [int] NOT NULL,
	[IdGrupo] [int] NOT NULL,
	[IdItemMenu] [int] NOT NULL,
	CONSTRAINT [PK_GruposItemsMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[GruposItemsMenu] TO  SCHEMA OWNER 
GO



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdGrupo] in table 'GruposUsuarios'
ALTER TABLE [dbo].[GruposItemsMenu]
ADD CONSTRAINT [FK_GruposItemsMenu_Grupos]
    FOREIGN KEY ([IdGrupo])
    REFERENCES [dbo].[Grupos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GruposUsuarios_Grupos'
CREATE INDEX [IX_FK_GruposItemsMenu_Grupos]
ON [dbo].[GruposItemsMenu]
    ([IdGrupo]);
GO

-- Creating foreign key on [IdUsuario] in table 'GruposUsuarios'
ALTER TABLE [dbo].[GruposItemsMenu]
ADD CONSTRAINT [FK_GruposItemsMenus_ItemsMenu]
    FOREIGN KEY ([IdItemMenu])
    REFERENCES [dbo].[ItemsMenu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GruposUsuarios_Grupos'
CREATE INDEX [IX_FK_GruposItemsMenu_ItemsMenu]
ON [dbo].[GruposItemsMenu]
    ([IdItemMenu]);
GO
