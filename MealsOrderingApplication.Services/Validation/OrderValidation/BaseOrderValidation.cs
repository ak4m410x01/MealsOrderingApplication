using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;

namespace MealsOrderingApplication.Services.Validation.OrderValidation
{
    public class BaseOrderValidation(IUnitOfWork unitOfWork) : IBaseOrderValidation
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> IsProductExists(int productId)
        {
            return ((await _unitOfWork.Products.GetByIdAsync(productId)) is not null);
        }
    }
}
