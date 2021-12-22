using CarShopApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShopApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole(name);

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            StringBuilder stringBuilder = new StringBuilder("Errors: ");

            foreach (var item in result.Errors)
            {
                stringBuilder.Append(item.Description + " | ");
            }

            ViewBag.Msg = stringBuilder.ToString();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageRole(string id, string msg = null)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction("Index");
            }

            ManageRoleViewModel roleViewModel = new ManageRoleViewModel();

            roleViewModel.Role = role;

            roleViewModel.UserHasRole = await _userManager.GetUsersInRoleAsync(role.Name);

            roleViewModel.UserNotRole = _userManager.Users.ToList();

            foreach (var item in roleViewModel.UserHasRole)
            {
                roleViewModel.UserNotRole.Remove(item);
            }

            ViewBag.Msg = msg;

            return View(roleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return RedirectToAction("Index");
            }
            
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ManageRole), new { msg = "Added user to Role", id = role.Id });
            }

            return RedirectToAction(nameof(ManageRole), new { msg = "Failed to add user to Role", id = role.Id });
        }
        
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return RedirectToAction("Index");
            }
            
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ManageRole), new { msg = "Removed user from Role", id = role.Id });
            }

            return RedirectToAction(nameof(ManageRole), new { msg = "Failed to remove user from Role", id = role.Id });
        }
    }
}
