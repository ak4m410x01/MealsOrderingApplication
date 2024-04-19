using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Meals;

namespace MealsOrderingApplication.Services.Repositories
{
    public class MealRepository : BaseRepository<Meal>, IMealRepository, IMealFilter
    {
        public MealRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Meal> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddMealDTO addDto)
            {
                return await Task.FromResult(new Meal
                {
                    Name = addDto.Name,
                    Description = addDto.Description,
                    Price = addDto.Price,
                    CategoryId = addDto.CategoryId,
                });
            }

            throw new ArgumentException("Invalid DTO type. Expected AddMealDTO.");
        }

        public override async Task<Meal> MapUpdateDtoToEntity<TDto>(Meal entity, TDto dto)
        {
            if (dto is UpdateMealDTO updateDto)
            {
                if (updateDto.Name is not null)
                    entity.Name = updateDto.Name;

                if (updateDto.Description is not null)
                    entity.Description = updateDto.Description;

                if (updateDto.Price is not null)
                    entity.Price = updateDto.Price ?? default;

                if (updateDto.CategoryId is not null)
                    entity.CategoryId = (int)updateDto.CategoryId;

                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateMealDTO.");
        }

        public virtual async Task<IQueryable<Meal>> FilterByNameAsync(IQueryable<Meal> meals, string name)
        {
            return await Task.FromResult(meals.Where(p => p.Name.Contains(name)));
        }

        public virtual async Task<IQueryable<Meal>> FilterByCategoryAsync(IQueryable<Meal> meals, int categoryId)
        {
            return await Task.FromResult(meals.Where(p => p.CategoryId == categoryId));
        }

        public virtual async Task<IQueryable<Meal>> FilterByPriceAsync(IQueryable<Meal> meals, double? minPrice, double? maxPrice)
        {
            if (minPrice is null || minPrice <= 0) minPrice = 1;
            if (maxPrice is null) maxPrice = double.MaxValue;

            if (minPrice > maxPrice)
            {
                double? tmp = minPrice;
                maxPrice = minPrice;
                minPrice = tmp;
            }

            return await Task.FromResult(meals.Where(p => p.Price >= minPrice && p.Price <= maxPrice));
        }
    }
}
