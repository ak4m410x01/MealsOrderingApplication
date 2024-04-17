using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using MealsOrderingApplication.Services.Validation.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.AdminValidation
{
    public class BaseAdminValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) :
        BaseApplicationUserValidation(unitOfWork, userManager),
        IBaseAdminValidation
    {
    }
}
