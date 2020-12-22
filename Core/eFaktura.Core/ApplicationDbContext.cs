using eFaktura.Core.Entities;
using EFaktura.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace eFaktura.Core
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        private DbSet<ClientEntity> Client { get; set; }

        private DbSet<CompanyEntity> Company { get; set; }
    }
}
