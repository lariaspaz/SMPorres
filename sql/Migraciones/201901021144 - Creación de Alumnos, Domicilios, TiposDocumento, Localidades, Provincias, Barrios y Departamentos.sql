
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/02/2019 11:20:45
-- Generated from EDMX file: C:\Proyectos\GitHub\SMPorres\src\SMPorres\Models\SMPorres.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SMPorres_Dev];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Alumnos_Domicilios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Alumnos] DROP CONSTRAINT [FK_Alumnos_Domicilios];
GO
IF OBJECT_ID(N'[dbo].[FK_Alumnos_TiposDocumento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Alumnos] DROP CONSTRAINT [FK_Alumnos_TiposDocumento];
GO
IF OBJECT_ID(N'[dbo].[FK_Barrios_Localidades]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Barrios] DROP CONSTRAINT [FK_Barrios_Localidades];
GO
IF OBJECT_ID(N'[dbo].[FK_Departamentos_Provincias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Departamentos] DROP CONSTRAINT [FK_Departamentos_Provincias];
GO
IF OBJECT_ID(N'[dbo].[FK_Domicilios_Barrios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Barrios];
GO
IF OBJECT_ID(N'[dbo].[FK_Domicilios_Departamentos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Departamentos];
GO
IF OBJECT_ID(N'[dbo].[FK_Domicilios_Localidades]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Localidades];
GO
IF OBJECT_ID(N'[dbo].[FK_Domicilios_Provincias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Domicilios] DROP CONSTRAINT [FK_Domicilios_Provincias];
GO
IF OBJECT_ID(N'[dbo].[FK_Localidades_Departamentos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Localidades] DROP CONSTRAINT [FK_Localidades_Departamentos];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Alumnos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Alumnos];
GO
IF OBJECT_ID(N'[dbo].[Barrios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Barrios];
GO
IF OBJECT_ID(N'[dbo].[Departamentos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departamentos];
GO
IF OBJECT_ID(N'[dbo].[Domicilios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domicilios];
GO
IF OBJECT_ID(N'[dbo].[Localidades]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Localidades];
GO
IF OBJECT_ID(N'[dbo].[Provincias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Provincias];
GO
IF OBJECT_ID(N'[dbo].[TiposDocumento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TiposDocumento];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Alumnos'
CREATE TABLE [dbo].[Alumnos] (
    [Id] int  NOT NULL,
    [Nombre] varchar(255)  NOT NULL,
    [Apellido] varchar(255)  NOT NULL,
    [IdTipoDocumento] int  NOT NULL,
    [NroDocumento] decimal(18,0)  NOT NULL,
    [FechaNacimiento] datetime  NOT NULL,
    [EMail] varchar(255)  NOT NULL,
    [Direccion] varchar(255)  NOT NULL,
    [IdDomicilio] int  NOT NULL,
    [Estado] tinyint  NOT NULL
);
GO

-- Creating table 'Barrios'
CREATE TABLE [dbo].[Barrios] (
    [Id] int  NOT NULL,
    [IdLocalidad] int  NOT NULL,
    [Nombre] nchar(10)  NULL
);
GO

-- Creating table 'Departamentos'
CREATE TABLE [dbo].[Departamentos] (
    [Id] int  NOT NULL,
    [IdProvincia] int  NOT NULL,
    [Nombre] varchar(50)  NOT NULL
);
GO

-- Creating table 'Domicilios'
CREATE TABLE [dbo].[Domicilios] (
    [Id] int  NOT NULL,
    [IdBarrio] int  NOT NULL,
    [IdLocalidad] int  NOT NULL,
    [IdDepartamento] int  NOT NULL,
    [IdProvincia] int  NOT NULL
);
GO

-- Creating table 'Localidads'
CREATE TABLE [dbo].[Localidads] (
    [Id] int  NOT NULL,
    [IdDepartamento] int  NOT NULL,
    [Nombre] varchar(50)  NOT NULL
);
GO

-- Creating table 'Provincias'
CREATE TABLE [dbo].[Provincias] (
    [Id] int  NOT NULL,
    [Nombre] varchar(50)  NOT NULL
);
GO

-- Creating table 'TipoDocumentoes'
CREATE TABLE [dbo].[TipoDocumentoes] (
    [Id] int  NOT NULL,
    [Descripcion] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Alumnos'
ALTER TABLE [dbo].[Alumnos]
ADD CONSTRAINT [PK_Alumnos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Barrios'
ALTER TABLE [dbo].[Barrios]
ADD CONSTRAINT [PK_Barrios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departamentos'
ALTER TABLE [dbo].[Departamentos]
ADD CONSTRAINT [PK_Departamentos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Domicilios'
ALTER TABLE [dbo].[Domicilios]
ADD CONSTRAINT [PK_Domicilios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Localidads'
ALTER TABLE [dbo].[Localidads]
ADD CONSTRAINT [PK_Localidads]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Provincias'
ALTER TABLE [dbo].[Provincias]
ADD CONSTRAINT [PK_Provincias]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TipoDocumentoes'
ALTER TABLE [dbo].[TipoDocumentoes]
ADD CONSTRAINT [PK_TipoDocumentoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdDomicilio] in table 'Alumnos'
ALTER TABLE [dbo].[Alumnos]
ADD CONSTRAINT [FK_Alumnos_Domicilios]
    FOREIGN KEY ([IdDomicilio])
    REFERENCES [dbo].[Domicilios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Alumnos_Domicilios'
CREATE INDEX [IX_FK_Alumnos_Domicilios]
ON [dbo].[Alumnos]
    ([IdDomicilio]);
GO

-- Creating foreign key on [IdTipoDocumento] in table 'Alumnos'
ALTER TABLE [dbo].[Alumnos]
ADD CONSTRAINT [FK_Alumnos_TiposDocumento]
    FOREIGN KEY ([IdTipoDocumento])
    REFERENCES [dbo].[TipoDocumentoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Alumnos_TiposDocumento'
CREATE INDEX [IX_FK_Alumnos_TiposDocumento]
ON [dbo].[Alumnos]
    ([IdTipoDocumento]);
GO

-- Creating foreign key on [IdLocalidad] in table 'Barrios'
ALTER TABLE [dbo].[Barrios]
ADD CONSTRAINT [FK_Barrios_Localidades]
    FOREIGN KEY ([IdLocalidad])
    REFERENCES [dbo].[Localidads]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Barrios_Localidades'
CREATE INDEX [IX_FK_Barrios_Localidades]
ON [dbo].[Barrios]
    ([IdLocalidad]);
GO

-- Creating foreign key on [IdBarrio] in table 'Domicilios'
ALTER TABLE [dbo].[Domicilios]
ADD CONSTRAINT [FK_Domicilios_Barrios]
    FOREIGN KEY ([IdBarrio])
    REFERENCES [dbo].[Barrios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Domicilios_Barrios'
CREATE INDEX [IX_FK_Domicilios_Barrios]
ON [dbo].[Domicilios]
    ([IdBarrio]);
GO

-- Creating foreign key on [IdProvincia] in table 'Departamentos'
ALTER TABLE [dbo].[Departamentos]
ADD CONSTRAINT [FK_Departamentos_Provincias]
    FOREIGN KEY ([IdProvincia])
    REFERENCES [dbo].[Provincias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Departamentos_Provincias'
CREATE INDEX [IX_FK_Departamentos_Provincias]
ON [dbo].[Departamentos]
    ([IdProvincia]);
GO

-- Creating foreign key on [IdDepartamento] in table 'Domicilios'
ALTER TABLE [dbo].[Domicilios]
ADD CONSTRAINT [FK_Domicilios_Departamentos]
    FOREIGN KEY ([IdDepartamento])
    REFERENCES [dbo].[Departamentos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Domicilios_Departamentos'
CREATE INDEX [IX_FK_Domicilios_Departamentos]
ON [dbo].[Domicilios]
    ([IdDepartamento]);
GO

-- Creating foreign key on [IdDepartamento] in table 'Localidads'
ALTER TABLE [dbo].[Localidads]
ADD CONSTRAINT [FK_Localidades_Departamentos]
    FOREIGN KEY ([IdDepartamento])
    REFERENCES [dbo].[Departamentos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Localidades_Departamentos'
CREATE INDEX [IX_FK_Localidades_Departamentos]
ON [dbo].[Localidads]
    ([IdDepartamento]);
GO

-- Creating foreign key on [IdLocalidad] in table 'Domicilios'
ALTER TABLE [dbo].[Domicilios]
ADD CONSTRAINT [FK_Domicilios_Localidades]
    FOREIGN KEY ([IdLocalidad])
    REFERENCES [dbo].[Localidads]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Domicilios_Localidades'
CREATE INDEX [IX_FK_Domicilios_Localidades]
ON [dbo].[Domicilios]
    ([IdLocalidad]);
GO

-- Creating foreign key on [IdProvincia] in table 'Domicilios'
ALTER TABLE [dbo].[Domicilios]
ADD CONSTRAINT [FK_Domicilios_Provincias]
    FOREIGN KEY ([IdProvincia])
    REFERENCES [dbo].[Provincias]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Domicilios_Provincias'
CREATE INDEX [IX_FK_Domicilios_Provincias]
ON [dbo].[Domicilios]
    ([IdProvincia]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------