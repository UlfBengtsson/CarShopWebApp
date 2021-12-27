using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Data
{
    internal class DbInitializer
    {
        internal static async Task Initialize(ShopDbContext context, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (context.Roles.Any())//seed check
            {
                return;
            }

            //seed in the following into the Db

            IdentityRole role = new IdentityRole("Admin");

            IdentityResult identityResult = await roleManager.CreateAsync(role);

            if (identityResult.Succeeded)
            {
                //Add user
                //add user to role
            }
            else
            {
                string errors = "";
                foreach (var item in identityResult.Errors)
                {
                    errors += item.Code + " | " + item.Description;
                }
                throw new Exception(errors);
            }

            context.Brands.Add(new Brand("SAAB"));
            context.SaveChanges();//don´t forget to save if your working with the database.
        }
    }
}
