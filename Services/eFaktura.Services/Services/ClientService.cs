using eFaktura.Abstractions;
using eFaktura.Core;
using EFaktura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Services.Services
{
    public class ClientService : GenericDataService, IClientService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public ClientService(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task CreateClient(ClientEntity clientEntity)
        {
            try
            {
                await AddAndSaveEntitiesAsync(clientEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteClient(string pdvNumber)
        {
            var clientEntity= await ReadSingleAsync<ClientEntity>(entity => entity.PdvNumber == pdvNumber);

            if (clientEntity == null)
                return;

            await RemoveAndSaveEntities(clientEntity);
        }

        /// <inheritdoc />
        public async Task<List<ClientEntity>> GetAllClients() => await ReadManyNoTrackingAsync<ClientEntity>();

        /// <inheritdoc />
        public async Task UpdateClient(ClientEntity clientEntity)
        {
            var currentEntity = await ReadSingleAsync<ClientEntity>(entity => entity.PdvNumber == clientEntity.PdvNumber);

            if (currentEntity == null)
                throw new ArgumentNullException(nameof(currentEntity));

            currentEntity.ModifiedDate = DateTime.Now;
            currentEntity.Name = clientEntity.Name;

            await Save();
        }
    }
}
