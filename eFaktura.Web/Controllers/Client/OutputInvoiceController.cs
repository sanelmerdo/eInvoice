using System;
using System.Collections.Generic;
using System.Linq;
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
            entity.CreateDate = DateTime.Now;

            await OutputInvoiceService.InsertOutputInvoice(entity);

            return Ok();
        }
    }
}