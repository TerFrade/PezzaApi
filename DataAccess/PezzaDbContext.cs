using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using PezzaApi.DataAccess.Models;

namespace DataAccess
{
    public class PezzaDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public PezzaDbContext(DbContextOptions<PezzaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pizza>()
                        .Property(p => p.Id)
                        .HasValueGenerator<SequentialGuidValueGenerator>();

            modelBuilder.Entity<Pizza>().HasData(Pizza.Seed);
        }
    }
}