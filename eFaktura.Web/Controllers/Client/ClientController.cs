using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eFaktura.Abstractions;
using EFaktura.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eFaktura.Services.Extensions;

namespace eFaktura.Web.Controllers.Client
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService ClientService;

        public ClientController(IClientService clientService) => ClientService = clientService;

        /// <summary>
        /// Retrieves all client entities.
        /// </summary>
        /// <returns>A collection of client entities as an asynchronous operation.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Core.Models.Client>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await ClientService.GetAllClients();
            var clients = result.Transform();
            return this.ToJsonResult(clients);
        }

        /// <summary>
        /// Create a company entity
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateClient([FromBody]ClientEntity clientEntity)
        {
            clientEntity.CreatedDate = DateTime.Now;

            await ClientService.CreateClient(clientEntity);

            return Ok();
        }

        [HttpDelete("delete/{pdvNumber}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClient(string pdvNumber)
        {
            await ClientService.DeleteClient(pdvNumber);

            return Ok();
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateClientEntity([FromBody] ClientEntity entity)
        {
            if (entity == null || string.IsNullOrWhiteSpace(entity.PdvNumber))
                throw new ArgumentNullException(nameof(entity));

            await ClientService.UpdateClient(entity);

            return Ok();
        }
    }
}