using System.Threading.Tasks;
using eFaktura.Abstractions;
using eFaktura.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eFaktura.Web.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvGeneratorController : ControllerBase
    {
        private IGenerateCsvService Service;

        private readonly IOptions<InputInvoice> InputInvoice;

        private readonly IOptions<OutputInvoice> OutputInvoice;

        public CsvGeneratorController(IGenerateCsvService service, 
                                    IOptions<InputInvoice> inputInvoice, 
                                    IOptions<OutputInvoice> outputInvoice)
        {
            Service = service;
            InputInvoice = inputInvoice;
            OutputInvoice = outputInvoice;
        }

        [HttpGet("input/clientId/{clientId}/taxPeriod/{taxPeriod}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateInputCsvFile(int clientId, string taxPeriod)
        {
            await Service.GenerateCsv(Core.InvoiceType.Input, taxPeriod, clientId, InputInvoice.Value.Path);
            return Ok();
        }

        [HttpGet("output/clientId/{clientId}/taxPeriod/{taxPeriod}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateOutputCsvFile(int clientId, string taxPeriod)
        {
            await Service.GenerateCsv(Core.InvoiceType.Output, taxPeriod, clientId, OutputInvoice.Value.Path);
            return Ok();
        }
    }
}