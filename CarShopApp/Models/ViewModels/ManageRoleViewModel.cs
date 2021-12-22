using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.ViewModels
{
    public class ManageRoleViewModel
    {
        public IdentityRole Role { get; set; }

        public IList<IdentityUser> UserHasRole { get; set; }
        public IList<IdentityUser> UserNotRole { get; set; }
    }
}
