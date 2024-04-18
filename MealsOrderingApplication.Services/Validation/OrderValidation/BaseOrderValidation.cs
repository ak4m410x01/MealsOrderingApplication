using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;

namespace MealsOrderingApplication.Services.Validation.OrderValidation
{
    public class BaseOrderValidation : IBaseOrderValidation
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseOrderValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsProductExistsAsync(int productId)
        {
            return ((await _unitOfWork.Products.GetByIdAsync(productId)) is not null);
        }
    }
}
