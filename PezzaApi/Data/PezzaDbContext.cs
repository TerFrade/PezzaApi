using Microsoft.EntityFrameworkCore;
using PezzaApi.Data.Models;

namespace PezzaApi.Data
{
    public class PezzaDbContext : DbContext
    {
        public PezzaDbContext(DbContextOptions<PezzaDbContext> options) : base(options)
        {
        }

        public DbSet<PezzaApi.Data.Models.PizzaModel> Pizza { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaModel>().HasData(PezzaApi.Data.Models.PizzaModel.Seed);
        }
    }
}