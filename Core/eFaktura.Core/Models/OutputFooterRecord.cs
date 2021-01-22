namespace eFaktura.Core.Models
{
    public class OutputFooterRecord
    {
        public int RecordType { get; set; }

        public decimal TotalInvoiceAmount { get; set; }

        public decimal TotalInternalInvoiceAmount { get; set; }

        public decimal TotalExportDeliveryAmount { get; set; }

        public decimal TotalInvoiceAmountForOtherDelivery { get; set; }

        public decimal TotalBasisAmountForCalculation { get; set; }

        public decimal TotalOutputPDV { get; set; }

        public decimal TotalBasicforCalulcationToNonRegisteredUser { get; set; }

        public decimal TotalOutputPDVToNonRegisteredUser { get; set; }

        public decimal TotalOutputPDV32 { get; set; }

        public decimal TotalOutputPDV33 { get; set; }

        public decimal TotalOutputPDV34 { get; set; }

        public decimal NumberOfRecords { get; set; }
    }
}
