using MealsOrderingApplication.Domain.DTOs.AuthanticationDTO;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace MealsOrderingApplication.Services.Services
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, JWTToken jwt, IAddCustomerValidation addCustomerValidation) : IAuthenticationService
    {
        protected readonly UserManager<ApplicationUser> _userManager = userManager;
        protected readonly JWTToken _jwt = jwt;
        protected readonly IAddCustomerValidation _addCustomerValidation = addCustomerValidation;

        public async Task<AuthanticationModel> RegisterAsync(RegisterDTO model)
        {
            string message = await _addCustomerValidation.AddIsValidAsync(new AddCustomerDTO()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
            });

            if (!string.IsNullOrEmpty(message))
                return new AuthanticationModel() { Message = message };

            Customer user = new()
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
                AuthanticationModel authModel = new();
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
                return new AuthanticationModel() { Message = "Email or Passwrod is Incorrect!" };

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
