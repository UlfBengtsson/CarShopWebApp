using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models
{
    public class BaseEntity<KeyType>
    {
        public KeyType Id { get; set; }
    }
}
