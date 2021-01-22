using eFaktura.Core;
using System.Threading.Tasks;

namespace eFaktura.Abstractions
{
    public interface IGenerateCsvService
    {
        Task<bool> GenerateCsv(InvoiceType invoiceType, string taxPeriod, int clientId, string filePath);
    }
}
