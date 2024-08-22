using Microsoft.EntityFrameworkCore;

namespace PezzaApi.Data
{
    public class PezzaDbContext : DbContext
    {
        public PezzaDbContext(DbContextOptions<PezzaDbContext> options) : base(options)
        {
        }
    }
}