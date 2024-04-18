using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        private readonly UserManager<ApplicationUser> _userManager;

        public override async Task<Customer> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddCustomerDTO addDto)
            {
                return await Task.FromResult(new Customer
                {
                    FirstName = addDto.FirstName,
                    LastName = addDto.LastName,
                    Email = addDto.Email,
                    UserName = addDto.Username,
                    PhoneNumber = addDto.PhoneNumber,
                    Location = addDto.Location,
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddCustomerDTO.");
        }

        public override async Task<Customer> MapUpdateDtoToEntity<TDto>(Customer entity, TDto dto)
        {
            if (dto is UpdateCustomerDTO updateDto)
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

                if (updateDto.PhoneNumber is not null)
                    entity.PhoneNumber = updateDto.PhoneNumber;

                if (updateDto.Location is not null)
                    entity.Location = updateDto.Location;


                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateCustomerDTO.");
        }

        public async Task<AuthanticationModel> CreateAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddCustomerDTO userDto)
            {
                if ((await _userManager.FindByEmailAsync(userDto.Email)) is not null)
                    return new AuthanticationModel() { Message = "Email is Already Exists!" };

                if ((await _userManager.FindByNameAsync(userDto.Username)) is not null)
                    return new AuthanticationModel() { Message = "Username is Already Exists!" };

                Customer user = await MapAddDtoToEntity(dto);

                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                {
                    AuthanticationModel authModel = new AuthanticationModel();
                    foreach (var error in result.Errors)
                    {
                        authModel.Message += error;
                    }
                    return authModel;
                }
                await _userManager.AddToRolesAsync(user, ["User", "Customer"]);


                return await Task.FromResult(new AuthanticationModel()
                {
                    IsAuthenticated = true,
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddCustomerDTO.");
        }
    }
}
