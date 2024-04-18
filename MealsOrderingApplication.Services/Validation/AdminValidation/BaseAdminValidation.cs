using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation;
using MealsOrderingApplication.Services.Validation.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.AdminValidation
{
    public class BaseAdminValidation : BaseApplicationUserValidation, IBaseAdminValidation
    {
        public BaseAdminValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }
    }
}
