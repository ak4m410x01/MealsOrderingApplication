using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Services.Validation.ProductValidation;

namespace DrinksOrderingApplication.Services.Validation.DrinkValidation
{
    public class BaseDrinkValidation : BaseProductValidation, IBaseDrinkValidation
    {
        public BaseDrinkValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
