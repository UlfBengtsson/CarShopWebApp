using CarShopApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Services
{
    public interface ICarsService
    {
        Car Create(CreateCarViewModel createCar);

        List<Car> GetAll();
        List<Car> FindByBrand(string brand);
        Car FindById(int id);

        void Edit(int id, CreateCarViewModel editCar);

        void Remove(int id);
    }
}
