﻿using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation;

namespace MealsOrderingApplication.Services.Validation.ProductValidation
{
    public class BaseProductValidation : IBaseProductValidation
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseProductValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsCategoryExistsAsync(int categoryId)
        {
            return (await _unitOfWork.Categories.GetByIdAsync(categoryId)) is not null;
        }
    }
}
