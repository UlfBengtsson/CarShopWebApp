using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public interface ICarsRepo : IGenericRepo<Car, int>
    {
        List<Car> GetByBrand(string brand);
    }
}
