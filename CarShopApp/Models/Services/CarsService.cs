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
        private readonly IInsuranceRepo _insuranceRepo;

        public CarsService(ICarsRepo carsRepo, IBrandRepo brandRepo, IInsuranceRepo insuranceRepo)
        {
            _carsRepo = carsRepo;
            _brandRepo = brandRepo;
            _insuranceRepo = insuranceRepo;
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

        public InsuranceCoverageViewModel InsuranceCoverage(Car car)
        {
            InsuranceCoverageViewModel insuranceCoverage = new InsuranceCoverageViewModel();
            insuranceCoverage.Car = car;

            List<Insurance> allInsurances = _insuranceRepo.GetAll();

            foreach (CarInsurance carInsurance in car.Insurances)
            {
                Insurance insurance = allInsurances.Single(ins => ins.Id == carInsurance.InsuranceId);
                insuranceCoverage.CurrentInsurances.Add(insurance);
                allInsurances.Remove(insurance);
            }

            insuranceCoverage.PossibleInsurances = allInsurances;

            return insuranceCoverage;
        }

        public void RemoveCarInsurance(Car car, int insuranceId)
        {
            CarInsurance insurance = car.Insurances.SingleOrDefault(carIns => carIns.InsuranceId == insuranceId);
            
            car.Insurances.Remove(insurance);
            
            _carsRepo.Update(car);
        }

        public void AddCarInsurance(Car car, int insuranceId)
        {
            CarInsurance insurance = new CarInsurance()
            {
                InsuranceId = insuranceId,
                CarId = car.Id
            };

            car.Insurances.Add(insurance);

            _carsRepo.Update(car);
        }
    }
}
