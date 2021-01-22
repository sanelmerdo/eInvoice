using System;
using System.Collections.Generic;
using System.Text;

namespace eFaktura.Core.Models
{
    public class HeaderRecord
    {
        public int HeaderType { get; set; }

        public string PdvNumber { get; set; }

        public string TaxPeriod { get; set; }

        public int FileType { get; set; }

        public string OridinalNumber { get; set; }

        public string DocumentDateCreated { get; set; }

        public string DocumentTimeCreated { get; set; }
    }
}
