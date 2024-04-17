using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.ProductValidation
{
    public class BaseProductValidation(IUnitOfWork unitOfWork) : IBaseProductValidation
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> IsCategoryExists(int categoryId)
        {
            return (await _unitOfWork.Categories.GetByIdAsync(categoryId)) is not null;
        }
    }
}
