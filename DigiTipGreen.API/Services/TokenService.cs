using DigiTipGreen.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigiTipGreen.API.Services
{
    public class TokenService
    {
        private readonly UserManager<User> usermanager;
        private readonly IConfiguration config;

        public TokenService(UserManager<User> usermanager, IConfiguration config)
        {
            this.usermanager = usermanager;
            this.config = config;
        }

        public async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Name, user.UserName),
            };

            var roles = await usermanager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim (ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:TokenKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenOptions = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
