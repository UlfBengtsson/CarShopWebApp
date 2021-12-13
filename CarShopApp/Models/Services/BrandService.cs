﻿using CarShopApp.Models.Repos;
using CarShopApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Services
{
    public class BrandService : IBrandService
    {
        private readonly IGenericRepo<Brand, int> _brandRepo;

        public BrandService(IGenericRepo<Brand, int> brandRepo)
        {
            _brandRepo = brandRepo;
        }

        public Brand Create(CreateBrandViewModel createBrand)
        {
            if (string.IsNullOrWhiteSpace(createBrand.Name))
            {
                return null;
            }

            return _brandRepo.Create(new Brand(createBrand.Name));
        }

        public bool Edit(int id, CreateBrandViewModel editBrand)
        {
            Brand originalBrand = FindById(id);

            if (originalBrand == null)
            {
                return false;
            }

            originalBrand.Name = editBrand.Name;

            return _brandRepo.Update(originalBrand);
        }

        public Brand FindById(int id)
        {
            return _brandRepo.GetById(id);
        }

        public List<Brand> GetAll()
        {
            return _brandRepo.GetAll();
        }

        public bool Remove(int id)
        {
            Brand brand = FindById(id);
            return _brandRepo.Delete(brand);
        }
    }
}
