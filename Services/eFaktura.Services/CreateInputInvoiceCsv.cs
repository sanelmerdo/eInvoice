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
    public class CreateInputInvoiceCsv : Invoice
    {
        private readonly int _fileType = 1;

        public CreateInputInvoiceCsv(ApplicationDbContext context,
                                        int clientId,
                                        string taxPeriod,
                                        string filePath) 
            : base(context, clientId, taxPeriod, filePath)
        {
        }

        public override async Task<bool> GenerateFile()
        {
            try
            {
                var firstRecord = await FindFirstRecord();
                var secondRecords = await FindSecondRecords();
                var thirdRecord = await FindThirdRecords();

                var filePath = Path.Combine(FilePath, GetFileName(InvoiceType.Input, firstRecord.FirstOrDefault().PdvNumber));
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
                        csv.WriteRecords<InputRecords>(secondRecords);
                        csv.WriteRecord<InputFooterRecord>(thirdRecord);

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

        private async Task<List<InputRecords>> FindSecondRecords() =>
            await ReadManyWithSelectNoTrackingAsync<InputInvoiceEntity, InputRecords>(
                entity => entity.ClientId == ClientId && entity.TaxPeriod == TaxPeriod,
                entity => new InputRecords
                {
                    RecordType = 2,
                    TaxPeriod = TaxPeriod,
                    SerialNumber = entity.SerialNumber,
                    DocumentType = entity.DocumentType,
                    InvoiceNumber = entity.InvoiceNumber,
                    DocumentDate = entity.DocumentDate,
                    DocumentReceivedDate = entity.DocumentReceivedDate,
                    CompanyName = entity.Company.Name,
                    CompanyAddress = entity.Company.Address,
                    CompanyPdvNumber = entity.Company.PdvNumber,
                    CompanyJibNumber = entity.Company.IdNumber,
                    InvoiceAmountWithoutPdv = entity.InvoiceAmountWithoutPdv,
                    InvoiceAmountWithPdv = entity.InvoiceAmountWithPdv,
                    FlatFreeAmount = entity.FlatFeeAmount,
                    InputPdvAmount = entity.InputPdvAmount,
                    InputPdvWhichCanBeRefused = entity.InputPdvWhichCanBeRefused,
                    InputPdvWhichCannotBeRefused = entity.InputPdvWhichCannotBeRefused,
                    InputPdv32 = entity.InputPdv32,
                    InputPdv33 = entity.InputPdv33,
                    InputPdv34 = entity.InputPdv34
                });

        private async Task<InputFooterRecord> FindThirdRecords()
        {
            var results = await ReadManyWithSelectNoTrackingAsync<InputInvoiceEntity, InputInvoiceEntity>(
                entity => entity.ClientId == ClientId && entity.TaxPeriod == TaxPeriod,
                entity => new InputInvoiceEntity
                {
                    InvoiceAmountWithoutPdv = entity.InvoiceAmountWithoutPdv,
                    InvoiceAmountWithPdv = entity.InvoiceAmountWithPdv,
                    FlatFeeAmount = entity.FlatFeeAmount,
                    InputPdvAmount = entity.InputPdvAmount,
                    InputPdvWhichCanBeRefused = entity.InputPdvWhichCanBeRefused,
                    InputPdvWhichCannotBeRefused = entity.InputPdvWhichCannotBeRefused,
                    InputPdv32 = entity.InputPdv32,
                    InputPdv33 = entity.InputPdv33,
                    InputPdv34 = entity.InputPdv34
                });

            return new InputFooterRecord
            {
                RecordType = 3,
                TotalInvoiceAmountWithoutPdv = results.Sum(x => x.InvoiceAmountWithoutPdv),
                TotalInvoiceAmountWithPdv = results.Sum(x => x.InvoiceAmountWithPdv),
                TotalFlatFeeAmount = results.Sum(x => x.FlatFeeAmount),
                TotalInputPdvAmount = results.Sum(x => x.InputPdvAmount),
                TotalInputPdvWhichCanBeRefused = results.Sum(x => x.InputPdvWhichCanBeRefused),
                TotalInputPdvWhichCannotBeRefused = results.Sum(x => x.InputPdvWhichCannotBeRefused),
                TotalInputPdv32 = results.Sum(x => x.InputPdv32),
                TotalInputPdv33 = results.Sum(x => x.InputPdv33),
                TotalInputPdv34 = results.Sum(x => x.InputPdv34),
                NumberOfRecords = results.Count
            };
        }
    }
}
