using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;

namespace MealsOrderingApplication.Services.Validation.ReviewValidation
{
    public class BaseReviewValidation : IBaseReviewValidation
    {
        public BaseReviewValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected readonly IUnitOfWork _unitOfWork;
    }
}
