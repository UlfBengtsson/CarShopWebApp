using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public interface IBrandRepo
    {
        //C
        Brand Create(Brand brand);

        //R
        List<Brand> Read();
        Brand Read(int id);

        //U
        bool Update(Brand brand);

        //D
        bool Delete(Brand brand);

    }
}
