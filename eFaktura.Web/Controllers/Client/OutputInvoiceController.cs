using System;
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
    public class OutputInvoiceController : ControllerBase
    {
        private IOutputInvoiceService OutputInvoiceService;

        public OutputInvoiceController(IOutputInvoiceService service) => OutputInvoiceService = service;

        /// <summary>
        /// Create a output invoice entity
        /// </summary>
        /// <param name="entity">Output invoice entity</param>
        /// <returns>A task as an asynchronous operation.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertOutputInvoice([FromBody] OutputInvoiceEntity entity)
        {
            await OutputInvoiceService.InsertOutputInvoice(entity);

            return Ok();
        }

        [HttpGet("client/{clientId}/taxperiod/{taxPeriod}")]
        [ProducesResponseType(typeof(IEnumerable<OutputInvoiceEntity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOutputInvoicesPerPeriodAndClientId(int clientId, string taxPeriod)
        {
            var result = await OutputInvoiceService.GetAllOutputInvoicesPerPeriodAndClientId(taxPeriod, clientId);

            return this.ToJsonResult(result);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOutputInvoice([FromBody] OutputInvoiceEntity entity)
        {
            await OutputInvoiceService.UpdateOutputInvoice(entity);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOutputInvoice(int id)
        {
            await OutputInvoiceService.DeleteOutputInvoice(id);

            return Ok();
        }
    }
}