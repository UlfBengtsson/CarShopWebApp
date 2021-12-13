using CarShopApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public class DatabaseInsuranceRepo : IInsuranceRepo
    {
        private readonly ShopDbContext _shopDbContext;

        public DatabaseInsuranceRepo(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public Insurance Create(Insurance insurance)
        {
            _shopDbContext.Insurances.Add(insurance);
            int result = _shopDbContext.SaveChanges();

            if (result > 0)
            {
                return insurance;
            }

            return null;
        }        

        public List<Insurance> GetAll()
        {
            return _shopDbContext.Insurances.ToList();
        }

        public Insurance GetById(int id)
        {
            return _shopDbContext.Insurances.SingleOrDefault(insurance => insurance.Id == id);
        }

        public void Update(Insurance insurance)
        {
            //_shopDbContext.Insurances.Update(insurance);
            _shopDbContext.Entry(insurance).State = EntityState.Modified;
            _shopDbContext.SaveChanges();
        }

        public void Delete(Insurance insurance)
        {
            _shopDbContext.Insurances.Remove(insurance);
            //_shopDbContext.Entry(insurance).State = EntityState.Deleted;
            _shopDbContext.SaveChanges();
        }
    }
}
