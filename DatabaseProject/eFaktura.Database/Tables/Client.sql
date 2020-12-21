﻿CREATE TABLE [dbo].[Client]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PdvNumber] [nvarchar](12) NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[ModifiedDate] [DateTime] NULL,
	CONSTRAINT [PK_Client_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO