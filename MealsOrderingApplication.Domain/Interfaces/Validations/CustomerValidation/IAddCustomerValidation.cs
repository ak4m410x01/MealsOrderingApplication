using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;

namespace MealsOrderingApplication.Domain.Interfaces.Validations.CustomerValidation
{
    public interface IAddCustomerValidation : IBaseCustomerValidation, IAddApplicationUserValidation
    {
    }
}
