using CarShopApp.Models.Repos;
using CarShopApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepo _insuranceRepo;

        public InsuranceService(IInsuranceRepo insuranceRepo)
        {
            _insuranceRepo = insuranceRepo;
        }

        public Insurance Create(CreateInsuranceViewModel createInsurance)
        {
            if (createInsurance == null)
            {
                return null;
            }
            Insurance insurance = new Insurance() { Name = createInsurance.Name, Covers = createInsurance.Covers };

            return _insuranceRepo.Create(insurance);
        }

        public bool Edit(int id, CreateInsuranceViewModel editInsurance)
        {
            Insurance insurance = FindById(id);

            if (insurance == null)
            {
                return false;
            }

            insurance.Name = editInsurance.Name;
            insurance.Covers = editInsurance.Covers;

            _insuranceRepo.Update(insurance);

            return true;
        }

        public Insurance FindById(int id)
        {
            return _insuranceRepo.GetById(id);
        }

        public List<Insurance> GetAll()
        {
            return _insuranceRepo.GetAll();
        }

        public bool Remove(int id)
        {
            Insurance insurance = FindById(id);

            if (insurance == null)
            {
                return false;
            }

            _insuranceRepo.Delete(insurance);

            return true;
        }
    }
}
