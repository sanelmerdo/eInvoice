using eFaktura.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace eFaktura.Core
{
    public abstract class Invoice : GenericDataService
    {
        protected int ClientId { get; private set; }

        protected string TaxPeriod { get; private set; }

        protected string FilePath { get; private set; }

        protected Invoice(DbContext context, int clientId, string taxPeriod, string filePath) 
            : base(context)
        {
            ClientId = clientId;
            TaxPeriod = taxPeriod;
            FilePath = filePath;
        }

        public abstract Task<bool> GenerateFile();

        protected string GetFileName(InvoiceType invoiceType, string pdvNumber)
        {
            return $"{pdvNumber}_{TaxPeriod}_{(int)invoiceType}_01.csv";
        }
    }
}
