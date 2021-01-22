using System;
using System.Collections.Generic;
using System.Text;

namespace eFaktura.Core.Models
{
    public class OutputRecord
    {
        public int RecordType { get; set; }

        public string TaxPeriod { get; set; }

        public string SerialNumber { get; set; }

        public string DocumentType { get; set; }

        public string InvoiceNumber { get; set; }

        public string DocumentDate { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPdvNumber { get; set; }

        public string CompanyJibNumber { get; set; }

        public decimal InvoiceAmount { get; set; }

        public decimal InternalInvoiceAmount { get; set; }

        public decimal ExportDeliveryAmount { get; set; }

        public decimal InvoiceAmountForOtherDelivery { get; set; }

        public decimal BasisAmountForCalculation { get; set; }

        public decimal OutputPDV { get; set; }

        public decimal BasicforCalulcationToNonRegisteredUser { get; set; }

        public decimal OutputPDVToNonRegisteredUser { get; set; }

        public decimal OutputPDV32 { get; set; }

        public decimal OutputPDV33 { get; set; }

        public decimal OutputPDV34 { get; set; }
    }
}
