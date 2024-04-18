using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.CustomerValidation
{
    public class UpdateCustomerValidation : BaseCustomerValidation, IUpdateCustomerValidation
    {
        public UpdateCustomerValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }

        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateCustomerDTO updateDto)
            {
                if ((updateDto.Email is not null) && (await IsEmailExistsAsync(updateDto.Email)))
                    return "Email is Already Exists!";

                if ((updateDto.Username is not null) && (await IsUsernameExistsAsync(updateDto.Username)))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateCustomerDTO.");
        }
    }
}
