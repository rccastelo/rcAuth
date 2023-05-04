using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rcAuthDomain.Entity;

namespace rcDbSqlServerEF
{
    public class ManagerDbContext : DbContext
    {
        private string _connectionString;
        private IConfiguration _configuration;

        public ManagerDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
