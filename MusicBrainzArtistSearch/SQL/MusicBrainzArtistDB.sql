-- Create Database
USE [master]
GO

IF EXISTS(SELECT * FROM sys.databases WHERE NAME='MusicBrainzDB')
BEGIN
	ALTER DATABASE [MusicBrainzDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [MusicBrainzDB]
END
GO

CREATE DATABASE [MusicBrainzDB]
GO

ALTER DATABASE [MusicBrainzDB] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MusicBrainzDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MusicBrainzDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MusicBrainzDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MusicBrainzDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [MusicBrainzDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MusicBrainzDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET RECOVERY FULL 
GO

ALTER DATABASE [MusicBrainzDB] SET  MULTI_USER 
GO

ALTER DATABASE [MusicBrainzDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MusicBrainzDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MusicBrainzDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MusicBrainzDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [MusicBrainzDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [MusicBrainzDB] SET QUERY_STORE = OFF
GO

USE [MusicBrainzDB]
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [MusicBrainzDB] SET  READ_WRITE 
GO


-- Create Tables
USE [MusicBrainzDB]
GO

IF (EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Artist'))
BEGIN
    DROP TABLE [dbo].[Artist]
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Artist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[artistname] [nvarchar](100) NOT NULL,
	[country] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Artist] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

IF (EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Identifier'))
BEGIN
    DROP TABLE [dbo].[Identifier]
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Identifier](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[artistid] [int] NOT NULL,
	[uniqueid] [nvarchar](100) NOT NULL,
	CONSTRAINT [PK_Identifier] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Identifier]  WITH CHECK ADD  CONSTRAINT [FK_Artist_ID] FOREIGN KEY([artistid])
REFERENCES [dbo].[Artist] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Identifier] CHECK CONSTRAINT [FK_Artist_ID]
GO


IF (EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Alias'))
BEGIN
    DROP TABLE [dbo].[Alias]
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Alias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[artistid] [int] NOT NULL,
	[aliasname] [nvarchar](100) NOT NULL,
	CONSTRAINT [PK_Alias] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Alias]  WITH CHECK ADD  CONSTRAINT [FK_Artist_Alias] FOREIGN KEY([artistid])
REFERENCES [dbo].[Artist] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Alias] CHECK CONSTRAINT [FK_Artist_Alias]
GO

-- Seed Artist data
INSERT dbo.Artist ([artistname],[country]) Values ('Metallica','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Lady Gaga','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Mumford & Sons','GB')
INSERT dbo.Artist ([artistname],[country]) Values ('Mott the Hoople','GB')
INSERT dbo.Artist ([artistname],[country]) Values ('Megadeth','US')
INSERT dbo.Artist ([artistname],[country]) Values ('John Coltrane','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Mogwai','GB')
INSERT dbo.Artist ([artistname],[country]) Values ('John Mayer','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Johnny Cash','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Jack Johnson','US')
INSERT dbo.Artist ([artistname],[country]) Values ('John Frusciante','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Elton John','GB')
INSERT dbo.Artist ([artistname],[country]) Values ('Rancid','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Transplants','US')
INSERT dbo.Artist ([artistname],[country]) Values ('Operation Ivy','US')

-- Seed ID data
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 1, '65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 2, '650e7db6-b795-4eb5-a702-5ea2fc46c848')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 3, 'c44e9c22-ef82-4a77-9bcd-af6c958446d6')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 4, '435f1441-0f43-479d-92db-a506449a686b')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 5, 'a9044915-8be3-4c7e-b11f-9e2d2ea0a91e')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 6, 'b625448e-bf4a-41c3-a421-72ad46cdb831')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 7, 'd700b3f5-45af-4d02-95ed-57d301bda93e')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 8, '144ef525-85e9-40c3-8335-02c32d0861f3')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 9, '18fa2fd5-3ef2-4496-ba9f-6dae655b2a4f')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 10, '6456a893-c1e9-4e3d-86f7-0008b0a3ac8a')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 11, 'f1571db1-c672-4a54-a2cf-aaa329f26f0b')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 12, 'b83bc61f-8451-4a5d-8b8e-7e9ed295e822')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 13, '24f8d8a5-269b-475c-a1cb-792990b0b2ee')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 14, '29f3e1bf-aec1-4d0a-9ef3-0cb95e8a3699')
INSERT dbo.Identifier ([artistid],[uniqueid]) VALUES ( 15, '931e1d1f-6b2f-4ff8-9f70-aa537210cd46')

-- Seed ID alias data
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (1, N'Metalica')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (1, N'메탈리카')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (2, N'Lady Ga Ga')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (2, N'Stefani Joanne Angelina Germanotta')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (4, N'Mott The Hoppie')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (4, N'Mott The Hopple')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (5, N'Megadeath')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (6, N'John Coltraine')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (6, N'John William Coltrane')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (7, N'Mogwa')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (9, N'Johhny Cash')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (9, N'Jonny Cash')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (10, N'Jack Hody Johnson')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (11, N'E. John')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (12, N'Elthon John')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (12, N'Elton Jphn')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (12, N'John Elton')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (12, N'Reginald Kenneth Dwight')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (13, N'ランシド')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (14, N'The Transplants')
INSERT dbo.Alias ([artistid], [aliasname]) VALUES (15, N'Op Ivy')
