using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.DriverDTO;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.DriverValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.DriverValidation
{
    public class AddDriverValidation : BaseDriverValidation, IAddDriverValidation
    {
        public AddDriverValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }

        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddDriverDTO addDto)
            {
                if (await IsEmailExistsAsync(addDto.Email))
                    return "Email is Already Exists!";

                if (await IsUsernameExistsAsync(addDto.Username))
                    return "Username is Already Exists!";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected AddDriverDTO.");
        }
    }
}
