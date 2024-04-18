using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;
using MealsOrderingApplication.Services.Validation.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.MealValidation
{
    public class BaseMealValidation : BaseProductValidation, IBaseMealValidation
    {
        public BaseMealValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
