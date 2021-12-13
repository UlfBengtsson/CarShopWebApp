using CarShopApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public class DatabaseInsuranceRepo : IGenericRepo<Insurance, int>//using a generic version of a Repo
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

        public bool Update(Insurance insurance)
        {
            //_shopDbContext.Insurances.Update(insurance);
            _shopDbContext.Entry(insurance).State = EntityState.Modified;
            int result = _shopDbContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(Insurance insurance)
        {
            _shopDbContext.Insurances.Remove(insurance);
            //_shopDbContext.Entry(insurance).State = EntityState.Deleted;
            int result = _shopDbContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
