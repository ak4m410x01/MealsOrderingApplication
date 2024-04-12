using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.Admin;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Models;
using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace MealsOrderingApplication.Services.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IAuthenticationService authService) : base(context)
        {
            _userManager = userManager;
            _authService = authService;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authService;

        public async Task<AuthanticationModel> AddAsync(AddAdminDTO model)
        {
            if ((await _userManager.FindByEmailAsync(model.Email)) is not null)
                return new AuthanticationModel() { Message = "Email is Already Exists!" };

            if ((await _userManager.FindByNameAsync(model.Username)) is not null)
                return new AuthanticationModel() { Message = "Username is Already Exists!" };

            Admin admin = new Admin()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username
            };

            IdentityResult result = await _userManager.CreateAsync(admin, model.Password);
            if (!result.Succeeded)
            {
                AuthanticationModel authModel = new AuthanticationModel();
                foreach (var error in result.Errors)
                {
                    authModel.Message += error;
                }
                return authModel;
            }

            await _userManager.AddToRolesAsync(admin, roles: new[] { "User", "Admin" });

            JwtSecurityToken jwtSecurityToken = await _authService.CreateJwtTokenAsync(admin);

            return await Task.FromResult(new AuthanticationModel()
            {
                IsAuthenticated = true,
                UserId = admin.Id,
                Email = admin.Email,
                UserName = admin.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            });
        }

        public override void Delete(Admin entity)
        {
            ApplicationUser? user = _context.Set<ApplicationUser>().Find(entity.Id);
            if (user is null)
                throw new NullReferenceException("Can't Find User");

            _context.Set<ApplicationUser>().Remove(user);
        }
        public override async Task DeleteAsync(Admin entity)
        {
            ApplicationUser? user = await _context.Set<ApplicationUser>().FindAsync(entity.Id);
            if (user is null)
                throw new NullReferenceException("Can't Find User");

            await Task.FromResult(_context.Set<ApplicationUser>().Remove(user));
        }
    }
}
