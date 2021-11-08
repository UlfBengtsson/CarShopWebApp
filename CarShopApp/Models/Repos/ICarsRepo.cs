using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public interface ICarsRepo
    {
        //C
        Car Create(Car car);

        //R
        List<Car> GetAll();
        List<Car> GetByBrand(string brand);
        Car GetById(int id);


        //U
        void Update(Car car);

        //D
        void Delete(Car car);
    }
}
