using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.ApplicationUsers;
using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository<ApplicationUser>, IApplicationUserFilter<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddApplicationUserValidation _addApplicationUserValidation;

        public ApplicationUserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAddApplicationUserValidation addApplicationUserValidation) : base(context)
        {
            _userManager = userManager;
            _addApplicationUserValidation = addApplicationUserValidation;
        }

        public override async Task<ApplicationUser> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddApplicationUserDTO addDto)
            {
                return await Task.FromResult(new ApplicationUser
                {
                    FirstName = addDto.FirstName,
                    LastName = addDto.LastName,
                    Email = addDto.Email,
                    UserName = addDto.Username,
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddApplicationUserDTO.");
        }

        public override async Task<ApplicationUser> MapUpdateDtoToEntity<TDto>(ApplicationUser entity, TDto dto)
        {
            if (dto is UpdateApplicationUserDTO updateDto)
            {
                if (updateDto.FirstName is not null)
                    entity.FirstName = updateDto.FirstName;

                if (updateDto.LastName is not null)
                    entity.LastName = updateDto.LastName;

                if (updateDto.Email is not null)
                    entity.Email = updateDto.Email;

                if (updateDto.Username is not null)
                    entity.UserName = updateDto.Username;

                if (updateDto.Password is not null)
                    await _userManager.ChangePasswordAsync(entity, entity.PasswordHash!, updateDto.Password);


                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected AddApplicationUserDTO.");
        }

        public async Task<AuthanticationModel> CreateAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddApplicationUserDTO userDto)
            {
                string message = await _addApplicationUserValidation.AddIsValidAsync(userDto);
                if (!string.IsNullOrEmpty(message))
                    return new AuthanticationModel() { Message = message };

                ApplicationUser user = await MapAddDtoToEntity(dto);

                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
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


                return await Task.FromResult(new AuthanticationModel()
                {
                    IsAuthenticated = true,
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddApplicationUserDTO.");
        }

        public virtual async Task<IQueryable<ApplicationUser>> FilterByEmailAsync(IQueryable<ApplicationUser> users, string email)
        {
            return await Task.FromResult(users.Where(u => u.Email == email));
        }

        public virtual async Task<IQueryable<ApplicationUser>> FilterByUsernameAsync(IQueryable<ApplicationUser> users, string username)
        {
            return await Task.FromResult(users.Where(u => u.UserName == username));
        }

        public virtual async Task<IQueryable<ApplicationUser>> FilterByNameAsync(IQueryable<ApplicationUser> users, string name)
        {
            return await Task.FromResult(users.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name)));
        }
    }
}
