using Microsoft.EntityFrameworkCore;

namespace ProductLib
{
    public interface IDbContext
    {
        DbSet<Coffe> Products { get; set; }

        int SaveChanges();
    }
}