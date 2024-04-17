using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.ApplicationUserValidation
{
    public class UpdateApplicationUserValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : BaseApplicationUserValidation(unitOfWork, userManager), IUpdateApplicationUserValidation
    {
        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateApplicationUserDTO updateDto)
            {
                if ((updateDto.Email is not null) && (await IsEmailExistsAsync(updateDto.Email)))
                    return "Email is Already Exists!";

                if ((updateDto.Username is not null) && (await IsUsernameExistsAsync(updateDto.Username)))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateApplicationUserDTO.");
        }
    }
}
