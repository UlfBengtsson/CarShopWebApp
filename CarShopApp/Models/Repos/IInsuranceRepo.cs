using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Repos
{
    public interface IInsuranceRepo
    {
        //C
        Insurance Create(Insurance insurance);

        //R
        List<Insurance> GetAll();
        Insurance GetById(int id);


        //U
        void Update(Insurance insurance);

        //D
        void Delete(Insurance insurance);
    }
}
