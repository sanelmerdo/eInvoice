CREATE TABLE [dbo].[Company]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PdvNumber] [nvarchar](12) NOT NULL,
	[IdNumber] [nvarchar](13) NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[ModifiedDate] [DateTime] NULL,
	[Address] [NVarchar] (100) NULL,
	CONSTRAINT [PK_Company_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO