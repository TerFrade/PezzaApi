using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using DataAccess.Models;

namespace DataAccess
{
    public class PezzaDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public PezzaDbContext(DbContextOptions<PezzaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                        .Property(p => p.Id)
                        .HasValueGenerator<SequentialGuidValueGenerator>();

            modelBuilder.Entity<Pizza>().HasData(Pizza.Seed);
        }
    }
}