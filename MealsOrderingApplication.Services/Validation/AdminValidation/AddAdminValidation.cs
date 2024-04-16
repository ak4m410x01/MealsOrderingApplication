using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.AdminDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.AdminValidation
{
    public class AddAdminValidation : BaseAdminValidation, IAddAdminValidation
    {
        public AddAdminValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }


        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddAdminDTO addDto)
            {
                if ((await _userManager.FindByEmailAsync(addDto.Email)) is not null)
                    return "Email is Already Exists!";

                if ((await _userManager.FindByNameAsync(addDto.Username)) is not null)
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddAdminDTO.");
        }
    }
}
