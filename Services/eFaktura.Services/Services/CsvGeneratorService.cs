using eFaktura.Abstractions;
using eFaktura.Core;
using System.Threading.Tasks;

namespace eFaktura.Services.Services
{
    public class CsvGeneratorService : IGenerateCsvService
    {
        private ApplicationDbContext Context;

        public CsvGeneratorService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<bool> GenerateCsv(InvoiceType invoiceType, string taxPeriod, int clientId, string filePath)
        {
            if(invoiceType == InvoiceType.Input)
            {
                return await new CreateInputInvoiceCsv(Context, clientId, taxPeriod, filePath).GenerateFile();
            }

            return await new CreateOutputInvoiceCsv(Context, clientId, taxPeriod, filePath).GenerateFile();
        }
    }
}
