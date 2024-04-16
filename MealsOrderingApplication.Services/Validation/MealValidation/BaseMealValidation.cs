using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;

namespace MealsOrderingApplication.Services.Validation.MealValidation
{
    public class BaseMealValidation : IBaseMealValidation
    {
        public BaseMealValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected readonly IUnitOfWork _unitOfWork;
    }
}
