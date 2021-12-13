using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.ViewModels
{
    public class CreateInsuranceViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Covers { get; set; }
    }
}
