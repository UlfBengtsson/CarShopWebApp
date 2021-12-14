using CarShopApp.Models.Repos;
using CarShopApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepo _carsRepo;
        private readonly IBrandRepo _brandRepo;

        public CarsService(ICarsRepo carsRepo, IBrandRepo brandRepo)
        {
            _carsRepo = carsRepo;
            _brandRepo = brandRepo;
        }

        public Car Create(CreateCarViewModel createCar)
        {
            if (string.IsNullOrWhiteSpace(createCar.ModelName))
            {
                throw new ArgumentException("Model and Brand may not just be consist of backspace(s)/whitespace(s)");
            }

            Car car = new Car() { 
                ModelName = createCar.ModelName, 
                BrandId = createCar.BrandId, 
                Price = createCar.Price 
            };

            car = _carsRepo.Create(car);

            return car;
        }
        public List<Car> GetAll()
        {
            return _carsRepo.GetAll();
        }
        public Car FindById(int id)
        {
            return _carsRepo.GetById(id);
        }

        public List<Car> FindByBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                return null;
            }
            return _carsRepo.GetByBrand(brand);
        }


        public void Edit(int id, CreateCarViewModel editCar)
        {
            Car car = _carsRepo.GetById(id);

            if (car != null)
            {
                car.ModelName = editCar.ModelName;
                car.Price = editCar.Price;
                car.BrandId = editCar.BrandId;
                car.Brand = _brandRepo.Read(editCar.BrandId);

                _carsRepo.Update(car);
            }
        }


        public void Remove(int id)
        {
            Car car = _carsRepo.GetById(id);
            if (car != null)
            {
                _carsRepo.Delete(car);
            }
        }

        public Car LastAdded()
        {
            List<Car> cars = _carsRepo.GetAll();
            if (cars.Count < 1)
            {
                return null;
            }
            return cars.Last();
        }
    }
}
