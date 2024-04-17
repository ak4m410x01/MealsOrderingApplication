using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.CustomerDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.CustomerValidation
{
    public class AddCustomerValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : BaseCustomerValidation(unitOfWork, userManager), IAddCustomerValidation
    {
        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddCustomerDTO addDto)
            {
                if (await IsEmailExists(addDto.Email))
                    return "Email is Already Exists!";

                if (await IsUsernameExists(addDto.Username))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddCustomerDTO.");
        }
    }
}
