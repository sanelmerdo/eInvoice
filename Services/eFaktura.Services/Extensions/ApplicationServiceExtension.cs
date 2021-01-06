using eFaktura.Abstractions;
using eFaktura.Core;
using eFaktura.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace eFaktura.Services.Extensions
{
    /// <summary>
    /// Provides extension methods for application services.
    /// </summary>
    public static class ApplicationServiceExtension
    {
        /// <summary>
        /// Registers application services.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>A service collection.</returns>
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            // Check params
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            // Add services
            services.TryAddScoped<IClientService, ClientService>();
            services.TryAddScoped<ICompanyService, CompanyService>();
            services.TryAddScoped<IOutputInvoiceService, OutputInvoiceService>();
            
            // Add service db context
            return services.AddDataServiceDbContext<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"));
        }

        private static IServiceCollection AddDataServiceDbContext<T>(this IServiceCollection services, string connectionString)
            where T : DbContext
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return services.AddDbContext<T>(options =>
            {
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(connectionString);
            });
        }
    }
}
