﻿using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.CustomerValidation
{
    public class AddCustomerValidation : BaseCustomerValidation, IAddCustomerValidation
    {
        public AddCustomerValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }


        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddCustomerDTO addDto)
            {
                if ((await _userManager.FindByEmailAsync(addDto.Email)) is not null)
                    return "Email is Already Exists!";

                if ((await _userManager.FindByNameAsync(addDto.Username)) is not null)
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddCustomerDTO.");
        }
    }
}
