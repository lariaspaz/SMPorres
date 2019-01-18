
/****** Object:  Table [dbo].[UsuariosItemsMenu]    Script Date: 07/01/2019 23:09:59 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND type in (N'U'))
DROP TABLE [dbo].[UsuariosItemsMenu]
GO
/****** Object:  Table [dbo].[GruposItemsMenu]    Script Date: 07/01/2019 23:09:59 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsuariosItemsMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsuariosItemsMenu](
	[Id] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdItemMenu] [int] NOT NULL,
	CONSTRAINT [PK_UsuariosItemsMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[UsuariosItemsMenu] TO  SCHEMA OWNER 
GO



-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdUsuario] in table 'GruposUsuarios'
ALTER TABLE [dbo].[UsuariosItemsMenu]
ADD CONSTRAINT [FK_UsuariosItemsMenu_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GruposUsuarios_Grupos'
CREATE INDEX [IX_FK_UsuariosItemsMenu_Usuarios]
ON [dbo].[UsuariosItemsMenu]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdUsuario] in table 'UsuariosItemsMenu'
ALTER TABLE [dbo].[UsuariosItemsMenu]
ADD CONSTRAINT [FK_UsuariosItemsMenu_ItemsMenu]
    FOREIGN KEY ([IdItemMenu])
    REFERENCES [dbo].[ItemsMenu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuariosItemsMenu_Grupos'
CREATE INDEX [IX_FK_UsuariosItemsMenu_ItemsMenu]
ON [dbo].[UsuariosItemsMenu]
    ([IdItemMenu]);
GO
