using DrinksOrderingApplication.Domain.Interfaces.Validations.DrinkValidation;
using MealsOrderingApplication.Domain;

namespace DrinksOrderingApplication.Services.Validation.DrinkValidation
{
    public class BaseDrinkValidation : IBaseDrinkValidation
    {
        public BaseDrinkValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected readonly IUnitOfWork _unitOfWork;
    }
}
