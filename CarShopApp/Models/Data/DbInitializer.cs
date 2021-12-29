using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopApp.Models.Data
{
    internal class DbInitializer
    {
        internal static async Task Initialize(
            ShopDbContext               context, 
            RoleManager<IdentityRole>   roleManager, 
            UserManager<IdentityUser>   userManager)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (context.Roles.Any())//seed check
            {
                return;
            }

            //seed in the following into the Db

            IdentityRole role = new IdentityRole("Admin");

            IdentityResult roleResult = await roleManager.CreateAsync(role);

            if (!roleResult.Succeeded)
            {
                MakeErrorMsgAndThrow(roleResult);
            }

            //Add user
            IdentityUser identityUser = new IdentityUser("Admin");
            IdentityResult userResult = await userManager.CreateAsync(identityUser, "Qwerty!23456");

            if (!userResult.Succeeded)
            {
                MakeErrorMsgAndThrow(userResult);
            }

            //add user to role
            IdentityResult userRoleResult = await userManager.AddToRoleAsync(identityUser, role.Name);

            if (!userRoleResult.Succeeded)
            {
                MakeErrorMsgAndThrow(userRoleResult);
            }

            context.Brands.Add(new Brand("SAAB"));
            context.SaveChanges();//don´t forget to save if your working with the database.
        }

        private static void MakeErrorMsgAndThrow(IdentityResult userRoleResult)
        {
            string errors = "";
            foreach (var item in userRoleResult.Errors)
            {
                errors += item.Code + " | " + item.Description;
            }
            throw new Exception(errors);
        }
    }
}
