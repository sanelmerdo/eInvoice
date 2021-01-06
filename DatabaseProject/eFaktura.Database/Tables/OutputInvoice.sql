CREATE TABLE [dbo].[OutputInvoice]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[TaxPeriod] [varchar] (4) NOT NULL,
	[OridinalNumber] [varchar] (2) NOT NULL,
	[SerialNumber] [nvarchar] (10) NULL,
	[DocumentType] [varchar] (2) NOT NULL,
	[InvoiceNumber] [nvarchar] (100) NULL,
	[DocumentDate] [varchar] (10) NULL,
	[CompanyId] [int] NOT NULL,
	[InvoiceAmount] [decimal] NOT NULL,
	[InternalInvoiceAmount] [decimal] NOT NULL,
	[ExportDeliveryAmount] [decimal] NOT NULL,
	[InvoiceAmountForOtherDelivery] [decimal] NOT NULL,
	[BasisAmountForCalculation] [decimal] NOT NULL,
	[OutputPDV] [decimal] NOT NULL,
	[BasicforCalulcationToNonRegisteredUser] [decimal] NOT NULL,
	[OutputPDVToNonRegisteredUser] [decimal] NOT NULL,
	[OutputPDV32] [decimal] NOT NULL,
	[OutputPDV33] [decimal] NOT NULL,
	[OutputPDV34] [decimal] NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[ModifiedDate] [DateTime] NULL,
	CONSTRAINT [PK_OutputInvoice_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO