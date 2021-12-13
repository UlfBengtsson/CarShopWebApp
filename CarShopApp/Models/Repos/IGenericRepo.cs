using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public interface IGenericRepo<EntityType, KeyType> where EntityType : BaseEntity<KeyType>
    {
        //C
        EntityType Create(EntityType entity);

        //R
        List<EntityType> GetAll();
        EntityType GetById(KeyType id);


        //U
        bool Update(EntityType entity);

        //D
        bool Delete(EntityType entity);
    }
}
