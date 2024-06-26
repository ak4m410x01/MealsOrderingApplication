﻿using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.MealValidation;

namespace MealsOrderingApplication.Services.Validation.MealValidation
{
    public class UpdateMealValidation : BaseMealValidation, IUpdateMealValidation
    {
        public UpdateMealValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateMealDTO updateDto)
            {
                if ((updateDto.CategoryId is not null) && (!(await IsCategoryExistsAsync(updateDto.CategoryId ?? default))))
                    return "Invalid Category Id";

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateMealDTO.");
        }
    }
}
