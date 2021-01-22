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
	[InvoiceAmount] [decimal](18, 2) NOT NULL,
	[InternalInvoiceAmount] [decimal](18, 2) NOT NULL,
	[ExportDeliveryAmount] [decimal](18, 2) NOT NULL,
	[InvoiceAmountForOtherDelivery] [decimal](18, 2) NOT NULL,
	[BasisAmountForCalculation] [decimal](18, 2) NOT NULL,
	[OutputPDV] [decimal](18, 2) NOT NULL,
	[BasicforCalulcationToNonRegisteredUser] [decimal](18, 2) NOT NULL,
	[OutputPDVToNonRegisteredUser] [decimal](18, 2) NOT NULL,
	[OutputPDV32] [decimal](18, 2) NOT NULL,
	[OutputPDV33] [decimal](18, 2) NOT NULL,
	[OutputPDV34] [decimal](18, 2) NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[ModifiedDate] [DateTime] NULL,
	CONSTRAINT [PK_OutputInvoice_Id] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO