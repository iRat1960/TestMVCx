CREATE DATABASE [TestDatabase]
GO

USE [TestDatabase];
GO

CREATE TABLE [dbo].[Users] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    [DateBirth] DATETIME      NOT NULL,
    [Gender]    BIT           NOT NULL,
    [ParentID]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Relatives] (
    [ChildId]  INT NOT NULL,
    [ParentId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ChildId] ASC),
    CONSTRAINT [FK_Relatives_Users_ParentID] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Users] ([Id])
);
GO