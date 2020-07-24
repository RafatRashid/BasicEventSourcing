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
    [Id] [NVARCHAR] (255) NOT NULL,
    [Name] [NVARCHAR] (255) NOT NULL,
    [AvailableQuantity] [NVARCHAR] (255) NOT NULL,
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
    [Id] [NVARCHAR](255) NOT NULL,
    [AggregateId] [NVARCHAR] (255) NOT NULL,
    [Payload] [NVARCHAR] (MAX) NULL,
    [Timestamp] [Datetime] NOT NULL,
    CONSTRAINT PK_EventStore PRIMARY KEY (Id)
  )
END
GO