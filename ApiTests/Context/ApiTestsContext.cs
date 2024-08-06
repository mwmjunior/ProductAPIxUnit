using ApiTests.Domains;
using Microsoft.EntityFrameworkCore;

namespace ApiTests.Context
{
    public class ApiTestsContext : DbContext
    {
        public ApiTestsContext() { }

        public ApiTestsContext(DbContextOptions<ApiTestsContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NOTE10-SALA21; Database= ApiTests; User Id=sa; Pwd=Senai@134; TrustServerCertificate= True");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
