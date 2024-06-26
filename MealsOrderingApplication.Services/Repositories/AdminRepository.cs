﻿using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Admins;
using MealsOrderingApplication.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository, IAdminFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public override async Task<Admin> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddAdminDTO addDto)
            {
                return await Task.FromResult(new Admin
                {
                    FirstName = addDto.FirstName,
                    LastName = addDto.LastName,
                    Email = addDto.Email,
                    UserName = addDto.Username,
                });
            }

            throw new ArgumentException("Invalid DTO type. Expected AddAdminDTO.");
        }

        public override async Task<Admin> MapUpdateDtoToEntity<TDto>(Admin entity, TDto dto)
        {
            if (dto is UpdateAdminDTO updateDto)
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
            throw new ArgumentException("Invalid DTO type. Expected UpdateAdminDTO.");
        }

        public async Task<AuthanticationModel> CreateAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddAdminDTO userDto)
            {
                Admin user = await MapAddDtoToEntity(dto);

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
                await _userManager.AddToRolesAsync(user, ["User", "Admin"]);


                return await Task.FromResult(new AuthanticationModel()
                {
                    IsAuthenticated = true,
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                });
            }
            throw new ArgumentException("Invalid DTO type. Expected AddAdminDTO.");
        }

        public virtual async Task<IQueryable<Admin>> FilterByEmailAsync(IQueryable<Admin> admins, string email)
        {
            return await Task.FromResult(admins.Where(u => u.Email == email));
        }

        public virtual async Task<IQueryable<Admin>> FilterByUsernameAsync(IQueryable<Admin> admins, string username)
        {
            return await Task.FromResult(admins.Where(u => u.UserName == username));
        }

        public virtual async Task<IQueryable<Admin>> FilterByNameAsync(IQueryable<Admin> admins, string name)
        {
            return await Task.FromResult(admins.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name)));
        }
    }
}
