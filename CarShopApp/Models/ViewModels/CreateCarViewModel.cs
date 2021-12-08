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
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public double Price { get; set; }

        public List<Brand> BrandList { get; set; }
    }
}
