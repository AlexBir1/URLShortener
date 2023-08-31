
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Roles;

namespace URLShortener.DataAccessLayer.JWT
{
    public class JWTService
    {
        private readonly UserManager<Account> _userManager;

        public JWTService(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateToken(JWTTokenProperty property)
        {
            try
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, property.Account.Id),
                };

                if (await _userManager.IsInRoleAsync(property.Account, AccountRoles.Admin.ToString()) == true)
                    userClaims.Add(new Claim(ClaimTypes.Role, AccountRoles.Admin.ToString()));
                else
                    userClaims.Add(new Claim(ClaimTypes.Role, AccountRoles.User.ToString()));

                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(property.Key));
                var credetials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(userClaims),
                    Expires = DateTime.UtcNow.AddDays(property.ExpiresInDays),
                    SigningCredentials = credetials,
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var JWT = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(JWT);

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            
        }

    }
}
