using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.DriverValidation;
using MealsOrderingApplication.Services.Validation.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.DriverValidation
{
    public class BaseDriverValidation : BaseApplicationUserValidation, IBaseDriverValidation
    {
        public BaseDriverValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }
    }
}
