using CarShopApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Services
{
    public interface IInsuranceService
    {
        Insurance Create(CreateInsuranceViewModel createInsurance);

        List<Insurance> GetAll();
        Insurance FindById(int id);

        bool Edit(int id, CreateInsuranceViewModel editInsurance);

        bool Remove(int id);
    }
}
