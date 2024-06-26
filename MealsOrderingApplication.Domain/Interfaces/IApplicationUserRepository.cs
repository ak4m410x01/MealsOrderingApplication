﻿using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.ApplicationUsers;
using MealsOrderingApplication.Domain.Models;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IApplicationUserRepository<TUser> : IBaseRepository<TUser>, IApplicationUserFilter<TUser> where TUser : ApplicationUser
    {
        Task<AuthanticationModel> CreateAsync<TDto>(TDto dto) where TDto : IAddDTO;
    }
}
