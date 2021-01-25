using eFaktura.Abstractions;
using eFaktura.Core;
using eFaktura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Services.Services
{
    public class CompanyService : GenericDataService, ICompanyService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyService"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public CompanyService(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task CreateCompany(CompanyEntity companyEntity) => await AddAndSaveEntitiesAsync(companyEntity);

        /// <inheritdoc />
        public async Task DeleteCompany(int id)
        {
            var currentEntity = await ReadSingleAsync<CompanyEntity>(entity => entity.Id == id);

            if (currentEntity == null)
                throw new ArgumentNullException(nameof(currentEntity));

            await RemoveAndSaveEntities(currentEntity);
        }

        /// <inheritdoc />
        public async Task<List<CompanyEntity>> GetAllCompanies(int clientId) 
            => await ReadManyWithSelectNoTrackingAsync<CompanyEntity, CompanyEntity>(entity => entity.ClientId == clientId,
                                                                                     entity => new CompanyEntity 
                                                                                     {
                                                                                        Id = entity.Id,
                                                                                        ClientId = entity.ClientId,
                                                                                        IdNumber = entity.IdNumber,
                                                                                        PdvNumber = entity.PdvNumber,
                                                                                        Name = entity.Name,
                                                                                        Address = entity.Address
                                                                                     });

        /// <inheritdoc />
        public async Task<CompanyEntity> GetCompanyById(int companyId) => await ReadSingleNoTrackingAsync<CompanyEntity>(entity => entity.Id == companyId);

        /// <inheritdoc />
        public async Task UpdateCompany(CompanyEntity companyEntity)
        {
            var currentEntity = await ReadSingleAsync<CompanyEntity>(entity => entity.Id == companyEntity.Id);

            if (currentEntity == null)
                throw new ArgumentNullException(nameof(currentEntity));

            currentEntity.ModifiedDate = DateTime.Now;
            currentEntity.Name = companyEntity.Name;
            currentEntity.PdvNumber = companyEntity.PdvNumber;
            currentEntity.IdNumber = companyEntity.IdNumber;
            currentEntity.Address = companyEntity.Address;

            await Save();
        }
    }
}
