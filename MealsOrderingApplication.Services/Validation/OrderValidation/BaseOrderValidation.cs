using MealsOrderingApplication.Domain;

namespace MealsOrderingApplication.Services.Validation.OrderValidation
{
    public class BaseOrderValidation
    {
        public BaseOrderValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected readonly IUnitOfWork _unitOfWork;
    }
}
