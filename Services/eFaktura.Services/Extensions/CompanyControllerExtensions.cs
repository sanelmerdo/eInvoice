using eFaktura.Core.Entities;
using eFaktura.Core.Models;
using System.Collections.Generic;

namespace eFaktura.Services.Extensions
{
    public static class CompanyControllerExtensions
    {
        public static List<Company> Transform(this List<CompanyEntity> entities)
        {
            List<Company> companies = new List<Company>();
            entities.ForEach(item =>
            {
                companies.Add(new Company
                {
                    Id = item.Id,
                    Name = item.Name,
                    PdvNumber = item.PdvNumber,
                    IdNumber = item.IdNumber
                });
            });

            return companies;
        }
    }
}
