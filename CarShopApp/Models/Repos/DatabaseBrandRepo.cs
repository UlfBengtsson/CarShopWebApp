using CarShopApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public class DatabaseBrandRepo : IBrandRepo
    {
        ShopDbContext _shopDbContext;

        public DatabaseBrandRepo(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public Brand Create(Brand brand)
        {
            _shopDbContext.Brands.Add(brand);
            _shopDbContext.SaveChanges();

            return brand;
        }

        public List<Brand> Read()
        {
            return _shopDbContext.Brands.ToList();
        }

        public Brand Read(int id)
        {
            return _shopDbContext.Brands.SingleOrDefault(brand => brand.Id == id);
        }

        public bool Update(Brand brand)
        {
            _shopDbContext.Brands.Update(brand);
            int result = _shopDbContext.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(Brand brand)
        {
            _shopDbContext.Brands.Remove(brand);
            int result = _shopDbContext.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
