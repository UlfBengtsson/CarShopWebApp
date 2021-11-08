using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        public string ModelName { get; set; }
        
        public string Brand { get; set; }

        public double Price { get; set; }
    }
}
