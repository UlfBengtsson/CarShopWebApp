using CarShopApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarShopApp.Models.Repos
{
    public class DatabaseCarsRepo : ICarsRepo
    {
        readonly ShopDbContext _shopDbContext;

        public DatabaseCarsRepo(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public Car Create(Car car)
        {
            _shopDbContext.Cars.Add(car);
            _shopDbContext.SaveChanges();
            return car;
        }

        public List<Car> GetAll()
        {
            return _shopDbContext.Cars.Include(car => car.Brand).ToList();
        }

        public List<Car> GetByBrand(string brand)
        {
            return _shopDbContext.Cars.Where( car => car.Brand.Name.Contains(brand) ).ToList();
        }

        public Car GetById(int id)
        {
            return _shopDbContext.Cars.Include(car => car.Brand).SingleOrDefault( car => car.Id == id);
        }

        public void Update(Car car)
        {
            _shopDbContext.Cars.Update(car);
            _shopDbContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            _shopDbContext.Cars.Remove(car);
            _shopDbContext.SaveChanges();
        }
    }
}
