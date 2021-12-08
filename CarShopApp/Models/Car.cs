using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        
        public string ModelName { get; set; }
        
        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public double Price { get; set; }
    }
}
