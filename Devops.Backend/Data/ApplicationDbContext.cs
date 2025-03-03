using Microsoft.EntityFrameworkCore;
using MyDotNetApi.Models;

namespace MyDotNetApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Calculation> Calculations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}