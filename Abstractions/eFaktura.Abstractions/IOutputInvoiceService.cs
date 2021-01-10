using eFaktura.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Abstractions
{
    public interface IOutputInvoiceService
    {
        /// <summary>
        /// Inserts output invoice entity.
        /// </summary>
        /// <param name="entity">Output entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task InsertOutputInvoice(OutputInvoiceEntity entity);

        /// <summary>
        /// Retrieves all output invoices per period and client id.
        /// </summary>
        /// <param name="period">Tax period.</param>
        /// <param name="clientId">Client id.</param>
        /// <returns>A collection of output invoice entities as an asynchronous operation.</returns>
        Task<List<OutputInvoiceEntity>> GetAllOutputInvoicesPerPeriodAndClientId(string period, int clientId);

        /// <summary>
        /// Updates output invoices.
        /// </summary>
        /// <param name="entity">Output entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task UpdateOutputInvoice(OutputInvoiceEntity entity);

        /// <summary>
        /// Deletes output invoice.
        /// </summary>
        /// <param name="id">output invoice id</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task DeleteOutputInvoice(int id);
    }
}
