namespace eFaktura.Core.Models
{
    public class InputFooterRecord
    {
        public int RecordType { get; set; }

        public decimal TotalInvoiceAmountWithoutPdv { get; set; }

        public decimal TotalInvoiceAmountWithPdv { get; set; }

        public decimal TotalFlatFeeAmount { get; set; }

        public decimal TotalInputPdvAmount { get; set; }

        public decimal TotalInputPdvWhichCanBeRefused { get; set; }

        public decimal TotalInputPdvWhichCannotBeRefused { get; set; }

        public decimal TotalInputPdv32 { get; set; }

        public decimal TotalInputPdv33 { get; set; }

        public decimal TotalInputPdv34 { get; set; }

        public int NumberOfRecords { get; set; }
    }
}
