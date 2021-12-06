using CarShopApp.Models.Data;
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
            throw new System.NotImplementedException();
        }

        public void Delete(Car car)
        {
            throw new System.NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _shopDbContext.Cars.ToList();
        }

        public List<Car> GetByBrand(string brand)
        {
            throw new System.NotImplementedException();
        }

        public Car GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Car car)
        {
            throw new System.NotImplementedException();
        }
    }
}
