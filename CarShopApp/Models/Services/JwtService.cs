﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarShopApp.Models.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(IdentityUser user, IList<string> userRoles, IEnumerable<Claim> claims)
        {
            List<Claim> claimsList = new List<Claim>();
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claimsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsList.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var role in userRoles)
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role));
            }

            claimsList.AddRange(claims);

            int expiraionDays = _configuration.GetValue<int>("JWTConfiguration:TokenExpirationDays");
            byte[] signingKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SigningKey"));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWTConfiguration:Issuer"),
                audience: _configuration.GetValue<string>("JWTConfiguration:Audience"),
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(expiraionDays),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
