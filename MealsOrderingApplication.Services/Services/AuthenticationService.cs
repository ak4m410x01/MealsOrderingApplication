using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.Helpers;
using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealsOrderingApplication.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(UserManager<ApplicationUser> userManager, JWT jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;

        public async Task<AuthanticationModel> RegisterAsync(RegisterDTO model)
        {
            if ((await _userManager.FindByEmailAsync(model.Email)) is not null)
                return new AuthanticationModel() { Message = "Email is Already Exists!" };

            if ((await _userManager.FindByNameAsync(model.Username)) is not null)
                return new AuthanticationModel() { Message = "Username is Already Exists!" };

            Customer customer = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.Phone,
                Location = model.Location,
            };

            IdentityResult result = await _userManager.CreateAsync(customer, model.Password);
            if (!result.Succeeded)
            {
                AuthanticationModel authModel = new AuthanticationModel();
                foreach (var error in result.Errors)
                {
                    authModel.Message += error;
                }
                return authModel;
            }

            await _userManager.AddToRoleAsync(customer, "User");

            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(customer);

            return await Task.FromResult(new AuthanticationModel()
            {
                IsAuthenticated = true,
                UserId = customer.Id,
                Email = customer.Email,
                UserName = customer.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            });
        }
        public async Task<AuthanticationModel> LoginAsync(LoginDTO model)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
                return new AuthanticationModel() { Message = "Username or Passwrod is Incorrect!" };

            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);

            return await Task.FromResult(new AuthanticationModel()
            {
                IsAuthenticated = true,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            });
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
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
