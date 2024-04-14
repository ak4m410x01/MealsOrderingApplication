using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace MealsOrderingApplication.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(UserManager<ApplicationUser> userManager, JWTToken jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTToken _jwt;

        public async Task<AuthanticationModel> RegisterAsync(RegisterDTO model)
        {
            if ((await _userManager.FindByEmailAsync(model.Email)) is not null)
                return new AuthanticationModel() { Message = "Email is Already Exists!" };

            if ((await _userManager.FindByNameAsync(model.Username)) is not null)
                return new AuthanticationModel() { Message = "Username is Already Exists!" };

            Customer user = new Customer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.Phone,
                Location = model.Location,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                AuthanticationModel authModel = new AuthanticationModel();
                foreach (var error in result.Errors)
                {
                    authModel.Message += error;
                }
                return authModel;
            }

            await _userManager.AddToRoleAsync(user, "User");

            JwtSecurityToken jwtSecurityToken = await _jwt.GenerateAccessTokenAsync(user);

            return await Task.FromResult(new AuthanticationModel()
            {
                IsAuthenticated = true,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            });
        }
        public async Task<AuthanticationModel> LoginAsync(LoginDTO model)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
                return new AuthanticationModel() { Message = "Username or Passwrod is Incorrect!" };

            JwtSecurityToken jwtSecurityToken = await _jwt.GenerateAccessTokenAsync(user);

            return await Task.FromResult(new AuthanticationModel()
            {
                IsAuthenticated = true,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            });
        }
    }
}
