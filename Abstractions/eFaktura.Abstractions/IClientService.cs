using EFaktura.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Abstractions
{
    /// <summary>
    /// Provides operations for clients.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Retrieve all client information.
        /// </summary>
        /// <returns>A collection of client entity as an asynchronous operation.</returns>
        Task<List<ClientEntity>> GetAllClients();

        /// <summary>
        /// Creates client entity.
        /// </summary>
        /// <param name="clientEntity">Client entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task CreateClient(ClientEntity clientEntity);

        /// <summary>
        /// Delete client entity.
        /// </summary>
        /// <param name="pdvNumber">Pdv number.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task DeleteClient(string pdvNumber);

        /// <summary>
        /// Updates client entity.
        /// </summary>
        /// <param name="entity">Client entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task UpdateClient(ClientEntity entity);
    }
}
