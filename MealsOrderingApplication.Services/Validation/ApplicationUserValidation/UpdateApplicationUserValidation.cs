using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.ApplicationUserValidation
{
    public class UpdateApplicationUserValidation : BaseApplicationUserValidation, IUpdateApplicationUserValidation
    {
        public UpdateApplicationUserValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }
        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateApplicationUserDTO updateDto)
            {
                if ((updateDto.Email is not null) && ((await _userManager.FindByEmailAsync(updateDto.Email)) is not null))
                    return "Email is Already Exists!";

                if ((updateDto.Username is not null) && ((await _userManager.FindByNameAsync(updateDto.Username)) is not null))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateApplicationUserDTO.");
        }
    }
}
