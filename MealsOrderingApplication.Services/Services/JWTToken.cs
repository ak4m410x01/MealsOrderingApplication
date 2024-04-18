using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Services.Helpers;
using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealsOrderingApplication.Services.Services
{
    public class JWTToken(UserManager<ApplicationUser> userManager, IOptions<JWTConfig> jwt) : IJWTToken
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly JWTConfig _jwt = jwt.Value;

        public virtual async Task<JwtSecurityToken> GenerateAccessTokenAsync(ApplicationUser user)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = [];

            foreach (string role in userRoles)
                roleClaims.Add(new Claim("roles", role));

            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("Email", user.Email!),
                .. userClaims,
                .. roleClaims,
            ];

            string jwtKey = _jwt.Key ?? throw new NullReferenceException("Missing JWT Key...!!");

            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtKey)),
                                SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);
        }
    }
}
