using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.ViewModels
{
    public class CreateCarViewModel
    {
        [Display(Name = "Model")]
        [Required]
        public string ModelName { get; set; }

        [Required]
        public string Brand { get; set; }

        public double Price { get; set; }

        public List<string> BrandList { 
            get {
                return new List<string>
                {
                    "SAAB", "BMW", "Volvo", "Seat", "Opel"
                };
            } }
    }
}
