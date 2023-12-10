using Microsoft.EntityFrameworkCore;
using ProductLib;
using ProductLib.Extensions;

namespace ProductApi;

public class MemoryDbContext : DbContext, IDbContext
{
    public MemoryDbContext(DbContextOptions<MemoryDbContext> options) : base(options)
    {
        Products = Set<Coffe>();
    }

    public DbSet<Coffe> Products { get; set; }
}
