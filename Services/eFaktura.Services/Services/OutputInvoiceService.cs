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

        public async Task<List<OutputInvoiceEntity>> GetAllOutputInvoicesPerPeriodAndClientId(string period, int clientId) 
            => await ReadManyWithSelectNoTrackingAsync<OutputInvoiceEntity, OutputInvoiceEntity>(
                entity => entity.ClientId == clientId && entity.TaxPeriod == period,
                entity => new OutputInvoiceEntity 
                {
                    Id = entity.Id,
                    BasicforCalulcationToNonRegisteredUser = entity.BasicforCalulcationToNonRegisteredUser,
                    BasisAmountForCalculation = entity.BasisAmountForCalculation,
                    ClientId = entity.ClientId,
                    CompanyId = entity.CompanyId,
                    DocumentDate = entity.DocumentDate,
                    DocumentType = entity.DocumentType,
                    TaxPeriod = entity.TaxPeriod,
                    SerialNumber = entity.SerialNumber,
                    InvoiceNumber = entity.InvoiceNumber,
                    InvoiceAmount = entity.InvoiceAmount,
                    InternalInvoiceAmount = entity.InternalInvoiceAmount,
                    ExportDeliveryAmount = entity.ExportDeliveryAmount,
                    InvoiceAmountForOtherDelivery = entity.InvoiceAmountForOtherDelivery,
                    OutputPDV = entity.OutputPDV,
                    OutputPDVToNonRegisteredUser = entity.OutputPDVToNonRegisteredUser,
                    OutputPDV32 = entity.OutputPDV32,
                    OutputPDV33 = entity.OutputPDV33,
                    OutputPDV34 = entity.OutputPDV34
                });

        public async Task InsertOutputInvoice(OutputInvoiceEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Check if number of records is greated than x
            entity.CreatedDate = DateTime.Now;
            entity.OridinalNumber = "01";

            await AddAndSaveEntitiesAsync(entity);
        }

        public async Task UpdateOutputInvoice(OutputInvoiceEntity entity)
        {
            var currentEntity = await ReadSingleAsync<OutputInvoiceEntity>(x => x.Id == entity.Id);

            if (currentEntity == null)
                throw new ArgumentNullException(nameof(currentEntity));

            currentEntity.CompanyId = entity.CompanyId;
            currentEntity.DocumentDate = entity.DocumentDate;
            currentEntity.BasisAmountForCalculation = entity.BasisAmountForCalculation;
            currentEntity.DocumentType = entity.DocumentType;
            currentEntity.ExportDeliveryAmount = entity.ExportDeliveryAmount;
            currentEntity.InvoiceAmountForOtherDelivery = entity.InvoiceAmountForOtherDelivery;
            currentEntity.InvoiceAmount = entity.InvoiceAmount;
            currentEntity.InvoiceNumber = entity.InvoiceNumber;
            currentEntity.InternalInvoiceAmount = entity.InternalInvoiceAmount;
            currentEntity.OutputPDV = entity.OutputPDV;
            currentEntity.OutputPDV32 = entity.OutputPDV32;
            currentEntity.OutputPDV33 = entity.OutputPDV33;
            currentEntity.OutputPDV34 = entity.OutputPDV34;
            currentEntity.BasicforCalulcationToNonRegisteredUser = entity.BasicforCalulcationToNonRegisteredUser;
            currentEntity.TaxPeriod = entity.TaxPeriod;
            currentEntity.OutputPDVToNonRegisteredUser = entity.OutputPDVToNonRegisteredUser;
            currentEntity.ModifiedDate = DateTime.Now;

            await Save();
        }
    }
}
