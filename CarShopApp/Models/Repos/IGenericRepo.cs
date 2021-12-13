using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public interface IGenericRepo<EntityType, KeyType>
    {
        //C
        EntityType Create(EntityType entityType);

        //R
        List<EntityType> GetAll();
        EntityType GetById(KeyType id);


        //U
        bool Update(EntityType car);

        //D
        bool Delete(EntityType car);
    }
}
