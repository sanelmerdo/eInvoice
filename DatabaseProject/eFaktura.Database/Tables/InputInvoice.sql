CREATE TABLE [dbo].[InputInvoice]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[TaxPeriod] [varchar] (4) NOT NULL,
	[OridinalNumber] [varchar] (2) NOT NULL,
	[SerialNumber] [nvarchar] (10) NULL,
	[DocumentType] [varchar] (2) NOT NULL,
	[InvoiceNumber] [nvarchar] (100) NULL,
	[DocumentDate] [varchar] (10) NULL,
	[DocumentReceivedDate] [varchar] (10) NULL,
	[CompanyId] [int] NOT NULL,
	[InvoiceAmountWithoutPdv] [decimal] NOT NULL,
	[InvoiceAmountWithPdv] [decimal] NOT NULL,
	[FlatFeeAmount] [decimal] NOT NULL,
	[InputPdvAmount] [decimal] NOT NULL,
	[InputPdvWhichCanBeRefused] [decimal] NOT NULL,
	[InputPdvWhichCannotBeRefused] [decimal] NOT NULL,
	[InputPdv32] [decimal] NOT NULL,
	[InputPdv33] [decimal] NOT NULL,
	[InputPdv34] [decimal] NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[ModifiedDate] [DateTime] NULL,
	CONSTRAINT [PK_InputInvoice_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO