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
    public class CompanyController : ControllerBase
    {
        public ICompanyService CompanyService;

        public CompanyController(ICompanyService service) => CompanyService = service;

        [HttpGet("client/{id}")]
        [ProducesResponseType(typeof(IEnumerable<CompanyEntity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCompanies(int id)
        {
            var result = await CompanyService.GetAllCompanies(id);
            //var companies = result.Transform();
            return this.ToJsonResult(result);
        }

        /// <summary>
        /// Create a company entity
        /// </summary>
        /// <param name="companyEntity"></param>
        /// <returns>A task as an asynchronous operation.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyEntity companyEntity)
        {
            companyEntity.CreatedDate = DateTime.Now;

            await CompanyService.CreateCompany(companyEntity);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await CompanyService.DeleteCompany(id);

            return Ok();
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await CompanyService.UpdateCompany(entity);

            return Ok();
        }
    }
}