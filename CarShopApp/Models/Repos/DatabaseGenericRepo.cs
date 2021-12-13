using CarShopApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public class DatabaseGenericRepo<EntityType, KeyType> : IGenericRepo<EntityType, KeyType> where EntityType : BaseEntity<KeyType>
    {
        private readonly ShopDbContext _shopDbContext;

        public DatabaseGenericRepo(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public EntityType Create(EntityType entity)
        {
            _shopDbContext.Set<EntityType>().Add(entity);
            int result = _shopDbContext.SaveChanges();

            if (result > 0)
            {
                return entity;
            }

            return default(EntityType);
        }

        public List<EntityType> GetAll()
        {
            return _shopDbContext.Set<EntityType>().ToList();
        }

        public EntityType GetById(KeyType id)
        {
            return _shopDbContext.Set<EntityType>().Find(id);
        }

        public bool Update(EntityType entity)
        {
            _shopDbContext.Set<EntityType>().Update(entity);
            int result = _shopDbContext.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(EntityType entity)
        {
            _shopDbContext.Set<EntityType>().Remove(entity);
            int result = _shopDbContext.SaveChanges();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
