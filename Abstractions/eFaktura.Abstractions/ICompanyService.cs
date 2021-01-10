using eFaktura.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eFaktura.Abstractions
{
    public interface ICompanyService
    {
        /// <summary>
        /// Retrieve all company information.
        /// </summary>
        /// <returns>A collection of company entity as an asynchronous operation.</returns>
        Task<List<CompanyEntity>> GetAllCompanies(int id);

        /// <summary>
        /// Creates company entity.
        /// </summary>
        /// <param name="companyEntity">Company entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task CreateCompany(CompanyEntity companyEntity);

        /// <summary>
        /// Delete company entity.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task DeleteCompany(int id);

        /// <summary>
        /// Updates company entity.
        /// </summary>
        /// <param name="entity">Company entity.</param>
        /// <returns>A task as an asynchronous operation.</returns>
        Task UpdateCompany(CompanyEntity entity);

        /// <summary>
        /// Retrieves company by company id.
        /// </summary>
        /// <param name="companyId">Company id.</param>
        /// <returns>A company entity as an asynchronous operation.</returns>
        Task<CompanyEntity> GetCompanyById(int companyId);
    }
}
