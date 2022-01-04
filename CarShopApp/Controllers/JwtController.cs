using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using CarShopApp.Models.Services;
using CarShopApp.Models.ViewModels;

namespace CarShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public JwtController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {

            var result = await _signInManager.PasswordSignInAsync(userName, password, false, true);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            IdentityUser user = await _userManager.FindByNameAsync(userName);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            string jwtToken = _jwtService.GenerateJwtToken(user, userRoles, User.Claims);

            return Ok(jwtToken);

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TestJwtToken()
        {
            return Ok("It works");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegViewModel userReg)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = userReg.UserName,
                    Email = userReg.Email,
                    PhoneNumber = userReg.Phone
                };
                IdentityResult result = await _userManager.CreateAsync(user, userReg.Password);

                if (result.Succeeded)
                {
                    return Ok("Created");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
