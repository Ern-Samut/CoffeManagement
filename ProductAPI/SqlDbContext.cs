using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductLib;
using ProductLib.Extensions;

namespace ProductApi;

public class SqlDbContext : DbContext, IDbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
        Products = Set<Coffe>();
    }

    public DbSet<Coffe> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfig());
        SeedProductData(modelBuilder.Entity<Coffe>());
    }

    private void SeedProductData(EntityTypeBuilder<Coffe> entityTypeBuilder)
    {
        var reqs = new List<CoffeCreateReq>()
        {
            new()
            {
                Code = "CffD001",
                Name = "Moca",
                Category= "Hot",
                Price=2
            },
            new()
            {
                Code = "CFFD002",
                Name = "Capucino",
                Category= "Ice",
                Price = 3
            },
        };
        entityTypeBuilder.HasData(reqs.Select(x => x.ToEntity()));
    }
}
