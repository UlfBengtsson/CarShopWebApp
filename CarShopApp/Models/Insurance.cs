using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models
{
    public class Insurance : BaseEntity<int>
    {

        public string Name { get; set; }

        public string Covers { get; set; }

        public List<CarInsurance> CarsCoverd { get; set; }

    }
}
