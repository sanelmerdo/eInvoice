using eFaktura.Abstractions;
using eFaktura.Core;
using eFaktura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Services.Services
{
    public class InputInvoiceService : GenericDataService, IInputInvoiceService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputInvoiceService"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public InputInvoiceService(ApplicationDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task DeleteInputInvoice(int id)
        {
            var recordToDelete = await ReadSingleAsync<InputInvoiceEntity>(entity => entity.Id == id);

            if (recordToDelete == null)
                throw new ArgumentNullException(nameof(recordToDelete));

            await RemoveAndSaveEntities(recordToDelete);
        }

        /// <inheritdoc />
        public async Task<List<InputInvoiceEntity>> GetAllInputInvoicesPerPeriodAndClientId(string period, int clientId) => await ReadManyWithSelectNoTrackingAsync<InputInvoiceEntity, InputInvoiceEntity>(
                entity => entity.TaxPeriod == period && entity.ClientId == clientId,
                entity => new InputInvoiceEntity
                {
                    Id = entity.Id,
                    ClientId = entity.ClientId,
                    TaxPeriod = entity.TaxPeriod,
                    OridinalNumber = entity.OridinalNumber,
                    SerialNumber = entity.SerialNumber,
                    InvoiceNumber = entity.InvoiceNumber,
                    DocumentType = entity.DocumentType,
                    DocumentDate = entity.DocumentDate,
                    DocumentReceivedDate = entity.DocumentReceivedDate,
                    CompanyId = entity.CompanyId,
                    InvoiceAmountWithPdv = entity.InvoiceAmountWithPdv,
                    InvoiceAmountWithoutPdv = entity.InvoiceAmountWithoutPdv,
                    FlatFeeAmount = entity.FlatFeeAmount,
                    InputPdvAmount = entity.InputPdvAmount,
                    InputPdvWhichCanBeRefused = entity.InputPdvWhichCanBeRefused,
                    InputPdvWhichCannotBeRefused = entity.InputPdvWhichCannotBeRefused,
                    InputPdv32 = entity.InputPdv32,
                    InputPdv33 = entity.InputPdv33,
                    InputPdv34 = entity.InputPdv34
                });

        /// <inheritdoc />
        public async Task InsertInputInvoice(InputInvoiceEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.OridinalNumber = "01";
            entity.CreatedDate = DateTime.Now;

            await AddAndSaveEntitiesAsync(entity);
        }

        /// <inheritdoc />
        public async Task UpdateInputInvoice(InputInvoiceEntity entity)
        {
            var currentEntity = await ReadSingleAsync<InputInvoiceEntity>(x => x.Id == entity.Id);

            if (currentEntity == null)
                throw new ArgumentNullException(nameof(currentEntity));

            currentEntity.InvoiceNumber = entity.InvoiceNumber;
            currentEntity.CompanyId = entity.CompanyId;
            currentEntity.DocumentDate = entity.DocumentDate;
            currentEntity.DocumentDate = entity.DocumentDate;
            currentEntity.DocumentType = entity.DocumentType;
            currentEntity.DocumentReceivedDate = entity.DocumentReceivedDate;
            currentEntity.InvoiceAmountWithPdv = entity.InvoiceAmountWithPdv;
            currentEntity.InvoiceAmountWithoutPdv = entity.InvoiceAmountWithoutPdv;
            currentEntity.FlatFeeAmount = entity.FlatFeeAmount;
            currentEntity.InputPdvAmount = entity.InputPdvAmount;
            currentEntity.InputPdvWhichCanBeRefused = entity.InputPdvWhichCanBeRefused;
            currentEntity.InputPdvWhichCannotBeRefused = entity.InputPdvWhichCannotBeRefused;
            currentEntity.InputPdv32 = entity.InputPdv32;
            currentEntity.InputPdv33 = entity.InputPdv33;
            currentEntity.InputPdv34 = entity.InputPdv34;
            currentEntity.ModifiedDate = DateTime.Now;

            await Save();
        }
    }
}
