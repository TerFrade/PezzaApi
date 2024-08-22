using Microsoft.EntityFrameworkCore;
using PezzaApi.Data.Models;

namespace PezzaApi.Data
{
    public class PezzaDbContext : DbContext
    {
        public PezzaDbContext(DbContextOptions<PezzaDbContext> options) : base(options)
        {
        }

        public DbSet<PezzaApi.Data.Models.Pizza> Pizza { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().HasData(PezzaApi.Data.Models.Pizza.Seed);
        }
    }
}