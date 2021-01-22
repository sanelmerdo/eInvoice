using CsvHelper;
using eFaktura.Core;
using eFaktura.Core.Entities;
using eFaktura.Core.Models;
using EFaktura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eFaktura.Services
{
    public class CreateOutputInvoiceCsv : Invoice
    {
        private readonly int _fileType = 2;

        public CreateOutputInvoiceCsv(ApplicationDbContext context, 
                                        int clientId, 
                                        string taxPeriod,
                                        string filePath)
            : base(context, clientId, taxPeriod, filePath)
        { }


        public override async Task<bool> GenerateFile()
        {
            try
            {
                var firstRecord = await FindFirstRecord();
                var secondRecords = await FindSecondRecords();
                var thirdRecord = await FindThirdRecords();
                var filePath = Path.Combine(FilePath, GetFileName(InvoiceType.Output, firstRecord.FirstOrDefault()?.PdvNumber));

                if (File.Exists(filePath))
                    File.Delete(filePath);

                if (secondRecords.Count < 32000)
                {
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.Delimiter = ",";
                        csv.Configuration.HasHeaderRecord = false;

                        csv.WriteRecords<HeaderRecord>(firstRecord);
                        csv.WriteRecords<OutputRecord>(secondRecords);
                        csv.WriteRecord<OutputFooterRecord>(thirdRecord);

                        writer.Flush();
                    }
                }
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        private async Task<List<HeaderRecord>> FindFirstRecord()
        {
            var curentDateTime = DateTime.Now;

            var result = await ReadSingleWithSelectNoTrackingAsync<ClientEntity, HeaderRecord>(
                entity => entity.Id == ClientId,
                entity => new HeaderRecord
                {
                    HeaderType = 1,
                    PdvNumber = entity.PdvNumber,
                    TaxPeriod = TaxPeriod,
                    FileType = _fileType,
                    OridinalNumber = "01",
                    DocumentDateCreated = curentDateTime.ToString("yyyy-MM-dd"),
                    DocumentTimeCreated = curentDateTime.ToString("HH:MM:ss")
                });

            return new List<HeaderRecord> { result };
        }

        private async Task<List<OutputRecord>> FindSecondRecords() => 
            await ReadManyWithSelectNoTrackingAsync<OutputInvoiceEntity, OutputRecord>(
                entity => entity.ClientId == ClientId && entity.TaxPeriod == TaxPeriod,
                entity => new OutputRecord
                {
                    RecordType = 2,
                    TaxPeriod = TaxPeriod,
                    SerialNumber = entity.SerialNumber,
                    DocumentType = entity.DocumentType,
                    InvoiceNumber = entity.InvoiceNumber,
                    DocumentDate = entity.DocumentDate,
                    CompanyName = entity.Company.Name,
                    CompanyAddress = entity.Company.Address,
                    CompanyPdvNumber = entity.Company.PdvNumber,
                    CompanyJibNumber = entity.Company.IdNumber,
                    InvoiceAmount = entity.InvoiceAmount,
                    InternalInvoiceAmount = entity.InternalInvoiceAmount,
                    ExportDeliveryAmount = entity.ExportDeliveryAmount,
                    InvoiceAmountForOtherDelivery = entity.InvoiceAmountForOtherDelivery,
                    BasisAmountForCalculation = entity.BasisAmountForCalculation,
                    OutputPDV = entity.OutputPDV,
                    BasicforCalulcationToNonRegisteredUser = entity.BasicforCalulcationToNonRegisteredUser,
                    OutputPDVToNonRegisteredUser = entity.OutputPDVToNonRegisteredUser,
                    OutputPDV32 = entity.OutputPDV32,
                    OutputPDV33 = entity.OutputPDV33,
                    OutputPDV34 = entity.OutputPDV34
                });

        private async Task<OutputFooterRecord> FindThirdRecords()
        {
            var results = await ReadManyWithSelectNoTrackingAsync<OutputInvoiceEntity, OutputInvoiceEntity>(
                entity => entity.ClientId == ClientId && entity.TaxPeriod == TaxPeriod,
                entity => new OutputInvoiceEntity
                {
                    InvoiceAmount = entity.InvoiceAmount,
                    InternalInvoiceAmount = entity.InternalInvoiceAmount,
                    ExportDeliveryAmount = entity.ExportDeliveryAmount,
                    InvoiceAmountForOtherDelivery = entity.InvoiceAmountForOtherDelivery,
                    BasisAmountForCalculation = entity.BasisAmountForCalculation,
                    OutputPDV = entity.OutputPDV,
                    BasicforCalulcationToNonRegisteredUser = entity.BasicforCalulcationToNonRegisteredUser,
                    OutputPDVToNonRegisteredUser = entity.OutputPDVToNonRegisteredUser,
                    OutputPDV32 = entity.OutputPDV32,
                    OutputPDV33 = entity.OutputPDV33,
                    OutputPDV34 = entity.OutputPDV34
                });

            return new OutputFooterRecord
            {
                RecordType = 3,
                TotalInvoiceAmount = results.Sum(x => x.InvoiceAmount),
                TotalInternalInvoiceAmount = results.Sum(x => x.InternalInvoiceAmount),
                TotalExportDeliveryAmount = results.Sum(x => x.ExportDeliveryAmount),
                TotalInvoiceAmountForOtherDelivery = results.Sum(x => x.InvoiceAmountForOtherDelivery),
                TotalBasisAmountForCalculation = results.Sum(x => x.BasisAmountForCalculation),
                TotalOutputPDV = results.Sum(x => x.OutputPDV),
                TotalBasicforCalulcationToNonRegisteredUser = results.Sum(x => x.BasicforCalulcationToNonRegisteredUser),
                TotalOutputPDVToNonRegisteredUser = results.Sum(x => x.OutputPDVToNonRegisteredUser),
                TotalOutputPDV32 = results.Sum(x => x.OutputPDV32),
                TotalOutputPDV33 = results.Sum(x => x.OutputPDV33),
                TotalOutputPDV34 = results.Sum(x => x.OutputPDV34),
                NumberOfRecords = results.Count()
            };
        }
    }
}
