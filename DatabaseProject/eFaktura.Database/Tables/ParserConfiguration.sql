CREATE TABLE [dbo].[ParserConfiguration]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceType] [int] NOT NULL,
	[RecordType] [int] NOT NULL,
	[ColumnIndex] [int] NOT NULL,
	[ColumnName] [varchar](100) NULL,
	[Snippet] [nvarchar](MAX) NULL
	CONSTRAINT [PK_ParserConfiguration_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO

/* 
InvoiceType: 1 - Nabavke, 2 - Isporuke
RecordType: 1 - Prvi slog, 2 - Drugi slog
*/