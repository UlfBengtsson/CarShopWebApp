using Microsoft.EntityFrameworkCore;

namespace CarShopApp.Models.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region CarInsurance Join Class Config

            modelBuilder.Entity<CarInsurance>().HasKey(ci =>
            new {
                ci.CarId,
                ci.InsuranceId
            });

            modelBuilder.Entity<CarInsurance>()
                .HasOne(ci => ci.Car)
                    .WithMany(c => c.Insurances)
                .HasForeignKey(ci => ci.CarId);

            modelBuilder.Entity<CarInsurance>()
                .HasOne(ci => ci.Insurance)
                    .WithMany(c => c.CarsCoverd)
                .HasForeignKey(ci => ci.InsuranceId);

            #endregion


        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<CarInsurance> CarInsurances { get; set; }//not mandatory

    }
}
