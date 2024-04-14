using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.CategoryDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Category> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddCategoryDTO addDto)
            {
                return await Task.FromResult(new Category
                {
                    Name = addDto.Name,
                    Description = addDto.Description,
                });
            }

            throw new ArgumentException("Invalid DTO type. Expected AddCategoryDTO.");
        }

        public override async Task<Category> MapUpdateDtoToEntity<TDto>(Category entity, TDto dto)
        {
            if (dto is UpdateCategoryDTO updateDto)
            {
                if (updateDto.Name is not null)
                    entity.Name = updateDto.Name;

                if (updateDto.Description is not null)
                    entity.Description = updateDto.Description;

                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateCategoryDTO.");
        }
    }
}
