using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Services.Helpers;
using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealsOrderingApplication.Services.Services
{
    public class JWTToken : IJWTToken
    {
        public JWTToken(UserManager<ApplicationUser> userManager, JWTConfig jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTConfig _jwt;
        public virtual async Task<JwtSecurityToken> GenerateAccessTokenAsync(ApplicationUser user)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = new List<Claim>();

            foreach (string role in userRoles)
                roleClaims.Append(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id),
                new Claim("Email", user.Email),
            }.Union(userClaims)
            .Union(roleClaims);

            // TODO: Fix Null Reference Exception Here
            string key = _jwt.Key is null ? "sz8eI7OdHBrjrIo8j9nTW/rQyO1OvY0pAQ2wDKQZw/0=" : _jwt.Key;
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);
        }
    }
}
