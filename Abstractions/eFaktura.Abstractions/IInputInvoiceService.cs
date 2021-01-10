using eFaktura.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Abstractions
{
    public interface IInputInvoiceService
    {
        /// <summary>
        /// Inserts input invoice entity.
        /// </summary>
        /// <param name="entity">Input entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task InsertInputInvoice(InputInvoiceEntity entity);

        /// <summary>
        /// Retrieves all input invoices per period and client id.
        /// </summary>
        /// <param name="period">Tax period.</param>
        /// <param name="clientId">Client id.</param>
        /// <returns>A collection of input invoice entities as an asynchronous operation.</returns>
        Task<List<InputInvoiceEntity>> GetAllInputInvoicesPerPeriodAndClientId(string period, int clientId);

        /// <summary>
        /// Updates input invoices.
        /// </summary>
        /// <param name="entity">Input entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task UpdateInputInvoice(InputInvoiceEntity entity);

        /// <summary>
        /// Deletes input invoice.
        /// </summary>
        /// <param name="id">Input invoice id</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task DeleteInputInvoice(int id);
    }
}
