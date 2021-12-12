
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/02/2018 20:40:29
-- Generated from EDMX file: F:\中專\CharmingNew\CharmingNew\Models\CharmingModel1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [charming];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order];
GO
IF OBJECT_ID(N'[dbo].[userN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[userN];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'orders'
CREATE TABLE [dbo].[orders] (
    [Email] nvarchar(100)  NOT NULL,
    [OrderBy] nvarchar(100)  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [PackDate] datetime  NOT NULL,
    [Number] int  NOT NULL,
    [HeadTable] int  NULL,
    [EndTable] int  NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'userNs'
CREATE TABLE [dbo].[userNs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [Password] nvarchar(80)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [OrderId] in table 'orders'
ALTER TABLE [dbo].[orders]
ADD CONSTRAINT [PK_orders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [Id] in table 'userNs'
ALTER TABLE [dbo].[userNs]
ADD CONSTRAINT [PK_userNs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------