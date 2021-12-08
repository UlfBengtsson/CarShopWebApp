using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarShopApp.Models.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(ShopDbContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (context.Brands.Any())//seed check
            {
                return;
            }

            //seed in the following into the Db

            //context.Cars.Add(new Car() { Brand = "SAAB", ModelName = "900 Turbo", Price = 123456.7 });
            context.Brands.Add(new Brand("SAAB"));
            context.SaveChanges();//don´t forget to save if your working with the database.
        }
    }
}
