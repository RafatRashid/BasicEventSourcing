IF DB_ID('PracticalEventSourcing') IS NULL BEGIN CREATE DATABASE PracticalEventSourcing
END
GO
  USE [PracticalEventSourcing] IF NOT EXISTS (
    SELECT
      *
    FROM
      sys.objects
    WHERE
      object_id = OBJECT_ID(N'dbo.[product]')
      AND type in (N'U')
  ) BEGIN CREATE TABLE [dbo].[Product] (
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [Name] [NVARCHAR] (255) NOT NULL,
    [AvailableQuantity] [INT] NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
  )
END
GO
  USE [PracticalEventSourcing] IF NOT EXISTS (
    SELECT
      *
    FROM
      sys.objects
    WHERE
      object_id = OBJECT_ID(N'dbo.[eventstore]')
      AND type in (N'U')
  ) BEGIN CREATE TABLE [dbo].[EventStore] (
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [AggregateId] [UNIQUEIDENTIFIER] NOT NULL,
    [Payload] [NVARCHAR] (MAX) NULL,
    [Timestamp] [Datetime] NOT NULL,
    [Version] [INT] NOT NULL,
    CONSTRAINT PK_EventStore PRIMARY KEY (Id)
  )
END
GO
  USE [PracticalEventSourcing] IF NOT EXISTS (
    SELECT
      *
    FROM
      sys.objects
    WHERE
      object_id = OBJECT_ID(N'dbo.[cart]')
      AND type in (N'U')
  ) BEGIN CREATE TABLE [dbo].[Cart] (
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [ProductIds] [NVARCHAR] (255) NOT NULL,
    [ShippingInformation] [NVARCHAR] (MAX) NULL,
    [CreatedAt] [Datetime] NOT NULL,
    CONSTRAINT PK_Cart PRIMARY KEY (Id)
  )
END
GO
  USE [PracticalEventSourcing] IF NOT EXISTS (
    SELECT
      *
    FROM
      sys.objects
    WHERE
      object_id = OBJECT_ID(N'dbo.[cartitem]')
      AND type in (N'U')
  ) BEGIN CREATE TABLE [dbo].[CartItem] (
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [CartId] [UNIQUEIDENTIFIER] NOT NULL,
    [ProductId] [UNIQUEIDENTIFIER] NULL,
    [Quantity] [Datetime] NOT NULL,
    CONSTRAINT PK_CartItem PRIMARY KEY (Id)
  )
END
GO