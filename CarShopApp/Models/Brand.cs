using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models
{
    public class Brand : BaseEntity<int>
    {

        [Required]
        [StringLength(80, MinimumLength = 1)]
        public string Name { get; set; }

        public List<Car> Cars { get; set; }

        public Brand() {}

        public Brand(string name)
        {
            Name = name;
        }

        //more brand related info
    }
}
