using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.ApplicationUserValidation
{
    public class BaseApplicationUserValidation : IBaseApplicationUserValidation
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly UserManager<ApplicationUser> _userManager;

        public BaseApplicationUserValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return (await _userManager.FindByEmailAsync(email)) is not null;
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return (await _userManager.FindByNameAsync(username)) is not null;
        }
    }
}
