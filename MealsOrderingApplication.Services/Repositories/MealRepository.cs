using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.MealDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class MealRepository : BaseRepository<Meal>, IMealRepository
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
    }
}
