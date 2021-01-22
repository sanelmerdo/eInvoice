using System;
using System.Collections.Generic;
using System.Text;

namespace eFaktura.Core.Models
{
    public class InputRecords
    {
        public int RecordType { get; set; }

        public string TaxPeriod { get; set; }

        public string SerialNumber { get; set; }

        public string DocumentType { get; set; }

        public string InvoiceNumber { get; set; }

        public string DocumentDate { get; set; }

        public string DocumentReceivedDate { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPdvNumber { get; set; }

        public string CompanyJibNumber { get; set; }

        public decimal InvoiceAmountWithoutPdv { get; set; }

        public decimal InvoiceAmountWithPdv { get; set; }

        public decimal FlatFreeAmount { get; set; }

        public decimal InputPdvAmount { get; set; }

        public decimal InputPdvWhichCanBeRefused { get; set; }

        public decimal InputPdvWhichCannotBeRefused { get; set; }

        public decimal InputPdv32 { get; set; }

        public decimal InputPdv33 { get; set; }

        public decimal InputPdv34 { get; set; }
    }
}
