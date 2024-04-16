using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.AdminValidation
{
    public class UpdateAdminValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : BaseAdminValidation(unitOfWork, userManager), IUpdateAdminValidation
    {
        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateAdminDTO updateDto)
            {
                if ((updateDto.Email is not null) && ((await _userManager.FindByEmailAsync(updateDto.Email)) is not null))
                    return "Email is Already Exists!";

                if ((updateDto.Username is not null) && ((await _userManager.FindByNameAsync(updateDto.Username)) is not null))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateAdminDTO.");
        }
    }
}
