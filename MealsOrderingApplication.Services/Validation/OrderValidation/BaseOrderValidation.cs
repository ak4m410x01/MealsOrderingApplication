using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;

namespace MealsOrderingApplication.Services.Validation.OrderValidation
{
    public class BaseOrderValidation : IBaseOrderValidation
    {
        public BaseOrderValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected readonly IUnitOfWork _unitOfWork;
    }
}
