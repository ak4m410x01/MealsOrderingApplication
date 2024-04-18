using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation;
using MealsOrderingApplication.Services.Validation.ApplicationUserValidation;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Validation.CustomerValidation
{
    public class BaseCustomerValidation :
        BaseApplicationUserValidation,
        IBaseCustomerValidation
    {
        public BaseCustomerValidation(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
        {
        }
    }
}
