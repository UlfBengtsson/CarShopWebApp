using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public class InMemoryCarsRepo : ICarsRepo
    {
        static int idCounter = 0;
        static List<Car> carsList = new List<Car>();

        public Car Create(Car car)
        {
            car.Id = ++idCounter;
            carsList.Add(car);
            return car;
        }

        public List<Car> GetAll()
        {
            return carsList;
        }

        public List<Car> GetByBrand(string brand)
        {
            List<Car> brandedCars = new List<Car>();

            foreach (Car aCar in carsList)
            {
                if (aCar.Brand.Name == brand)
                {
                    brandedCars.Add(aCar);
                }
            }

            return brandedCars;
        }

        public Car GetById(int id)
        {
            Car car = null;

            foreach (Car aCar in carsList)
            {
                if (aCar.Id == id)
                {
                    car = aCar;
                    break;
                }
            }

            return car;
        }

        public bool Update(Car car)
        {
            Car orignialCar = GetById(car.Id);

            if (orignialCar != null)
            {
                orignialCar.ModelName = car.ModelName;
                orignialCar.Brand = car.Brand;
                orignialCar.Price = car.Price;

                return true;
            }
            return false;
        }
        public bool Delete(Car car)
        {
            if (car != null)
            {
                return carsList.Remove(car);
            }

            return false;
        }
    }
}
