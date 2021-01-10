using System.Collections.Generic;
using System.Threading.Tasks;
using eFaktura.Abstractions;
using eFaktura.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eFaktura.Web.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputInvoiceController : ControllerBase
    {
        private IInputInvoiceService InputInvoiceService;

        public InputInvoiceController(IInputInvoiceService service) => InputInvoiceService = service;

        /// <summary>
        /// Create a input invoice entity
        /// </summary>
        /// <param name="entity">Input invoice entity</param>
        /// <returns>A task as an asynchronous operation.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertInputInvoice([FromBody] InputInvoiceEntity entity)
        {
            await InputInvoiceService.InsertInputInvoice(entity);

            return Ok();
        }

        [HttpGet("client/{clientId}/taxperiod/{taxPeriod}")]
        [ProducesResponseType(typeof(IEnumerable<InputInvoiceEntity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInputInvoicesPerPeriodAndClientId(int clientId, string taxPeriod)
        {
            var result = await InputInvoiceService.GetAllInputInvoicesPerPeriodAndClientId(taxPeriod, clientId);

            return this.ToJsonResult(result);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOutputInvoice([FromBody] InputInvoiceEntity entity)
        {
            await InputInvoiceService.UpdateInputInvoice(entity);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteInputInvoice(int id)
        {
            await InputInvoiceService.DeleteInputInvoice(id);

            return Ok();
        }
    }
}