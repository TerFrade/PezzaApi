using Microsoft.EntityFrameworkCore;
using PezzaApi.Data.Models;

namespace PezzaApi.Data
{
    public class PezzaDbContext : DbContext
    {
        public PezzaDbContext(DbContextOptions<PezzaDbContext> options) : base(options) { }



    }
}
