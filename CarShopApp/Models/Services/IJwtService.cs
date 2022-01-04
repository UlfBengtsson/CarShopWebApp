using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarShopApp.Models.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(IdentityUser user, IList<string> userRoles, IEnumerable<Claim> claims);
    }
}
