using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.ViewModels
{
    public class CreateBrandViewModel
    {
        [Required]
        [Display(Name = "Brand name")]
        [StringLength(80, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
