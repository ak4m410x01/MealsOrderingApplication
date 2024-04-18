using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.ReviewValidation;

namespace MealsOrderingApplication.Services.Validation.ReviewValidation
{
    public class BaseReviewValidation : IBaseReviewValidation
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseReviewValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsProductExistsAsync(int productId)
        {
            return ((await _unitOfWork.Products.GetByIdAsync(productId)) is not null);
        }

        public async Task<bool> IsCustomerExistsAsync(string customerId)
        {
            return ((await _unitOfWork.Customers.GetByIdAsync(customerId)) is not null);
        }
    }
}
