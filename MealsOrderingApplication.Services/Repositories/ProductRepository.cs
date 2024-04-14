using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.ProductDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Product> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddProductDTO addDto)
            {
                return await Task.FromResult(new Product
                {
                    Name = addDto.Name,
                    Description = addDto.Description,
                    Price = addDto.Price,
                    CategoryId = addDto.CategoryId,
                });
            }

            throw new ArgumentException("Invalid DTO type. Expected AddProductDTO.");
        }

        public override async Task<Product> MapUpdateDtoToEntity<TDto>(Product entity, TDto dto)
        {
            if (dto is UpdateProductDTO updateDto)
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
            throw new ArgumentException("Invalid DTO type. Expected UpdateProductDTO.");
        }
    }
}
