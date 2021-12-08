using CarShopApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Services
{
    public interface IBrandService
    {
        Brand Create(CreateBrandViewModel createBrand);

        List<Brand> GetAll();
        Brand FindById(int id);

        bool Edit(int id, CreateBrandViewModel editBrand);

        bool Remove(int id);
    }
}
