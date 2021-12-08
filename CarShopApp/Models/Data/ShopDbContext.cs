using Microsoft.EntityFrameworkCore;

namespace CarShopApp.Models.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }
    }
}
