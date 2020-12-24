using eFaktura.Abstractions;
using eFaktura.Core;
using eFaktura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Services.Services
{
    public class OutputInvoiceService : GenericDataService, IOutputInvoiceService
    {
        public OutputInvoiceService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<OutputInvoiceEntity>> GetAllOutputInvoicesPerPeriodAndClientId(string period, int clientId) => await ReadManyAsync<OutputInvoiceEntity>(entity => entity.ClientId == clientId && entity.TaxPeriod == period);

        public async Task InsertOutputInvoice(OutputInvoiceEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await AddAndSaveEntitiesAsync(entity);
        }

        public async Task UpdateOutputInvoice(OutputInvoiceEntity entity)
        {
            var currentEntity = await ReadSingleAsync<OutputInvoiceEntity>(x => x.Id == entity.Id);

            if (currentEntity == null)
                throw new ArgumentNullException(nameof(currentEntity));

            currentEntity.CompanyId = entity.CompanyId;
            currentEntity.DocumentDate = entity.DocumentDate;
            currentEntity.DocumentReceivedDate = entity.DocumentReceivedDate;
            currentEntity.DocumentType = entity.DocumentType;
            currentEntity.FileNumber = entity.FileNumber;
            currentEntity.FlatRateFeeAmount = entity.FlatRateFeeAmount;
            currentEntity.InputPdvAmount = entity.InputPdvAmount;
            currentEntity.InvoiceAmountWithoutPDV = entity.InvoiceAmountWithoutPDV;
            currentEntity.InvoiceNumber = entity.InvoiceNumber;
            currentEntity.InvooiceAmountWithPDV = entity.InvooiceAmountWithPDV;
            currentEntity.NonRefusedInputPdv = entity.NonRefusedInputPdv;
            currentEntity.NonRefusedInputPdv32 = entity.NonRefusedInputPdv32;
            currentEntity.NonRefusedInputPdv33 = entity.NonRefusedInputPdv33;
            currentEntity.NonRefusedInputPdv34 = entity.NonRefusedInputPdv34;
            currentEntity.RefusedInputPdvAmout = entity.RefusedInputPdvAmout;
            currentEntity.SerialNumber = entity.SerialNumber;
            currentEntity.TaxPeriod = entity.TaxPeriod;

            await Save();
        }
    }
}
