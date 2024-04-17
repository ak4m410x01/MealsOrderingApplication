using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.AdminValidation
{
    public class AddAdminValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : BaseAdminValidation(unitOfWork, userManager), IAddAdminValidation
    {
        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddAdminDTO addDto)
            {
                if (await IsEmailExistsAsync(addDto.Email))
                    return "Email is Already Exists!";

                if (await IsUsernameExistsAsync(addDto.Username))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddAdminDTO.");
        }
    }
}
