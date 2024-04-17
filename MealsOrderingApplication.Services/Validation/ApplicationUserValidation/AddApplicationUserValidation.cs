using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.ApplicationUserValidation
{
    public class AddApplicationUserValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) :
        BaseApplicationUserValidation(unitOfWork, userManager),
        IAddApplicationUserValidation
    {
        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddApplicationUserDTO addDto)
            {
                if (await IsEmailExists(addDto.Email))
                    return "Email is Already Exists!";

                if (await IsUsernameExists(addDto.Username))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddApplicationUserDTO.");
        }
    }
}
