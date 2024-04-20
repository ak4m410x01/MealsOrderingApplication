using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.DriverDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Drivers;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Repositories
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository, IDriverFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DriverRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public override async Task<Driver> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddDriverDTO addDto)
            {
                return await Task.FromResult(new Driver
                {
                    FirstName = addDto.FirstName,
                    LastName = addDto.LastName,
                    Email = addDto.Email,
                    UserName = addDto.Username,
                });
            }

            throw new ArgumentException("Invalid DTO type. Expected AddDriverDTO.");
        }

        public override async Task<Driver> MapUpdateDtoToEntity<TDto>(Driver entity, TDto dto)
        {
            if (dto is UpdateDriverDTO updateDto)
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
            throw new ArgumentException("Invalid DTO type. Expected UpdateDriverDTO.");
        }

        public async Task<AuthanticationModel> CreateAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddDriverDTO userDto)
            {
                Driver user = await MapAddDtoToEntity(dto);

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
                await _userManager.AddToRolesAsync(user, ["User", "Driver"]);


                return await Task.FromResult(new AuthanticationModel()
                {
                    IsAuthenticated = true,
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddDriverDTO.");
        }

        public virtual async Task<IQueryable<Driver>> FilterByEmailAsync(IQueryable<Driver> Drivers, string email)
        {
            return await Task.FromResult(Drivers.Where(u => u.Email == email));
        }

        public virtual async Task<IQueryable<Driver>> FilterByUsernameAsync(IQueryable<Driver> Drivers, string username)
        {
            return await Task.FromResult(Drivers.Where(u => u.UserName == username));
        }

        public virtual async Task<IQueryable<Driver>> FilterByNameAsync(IQueryable<Driver> Drivers, string name)
        {
            return await Task.FromResult(Drivers.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name)));
        }
    }
}
