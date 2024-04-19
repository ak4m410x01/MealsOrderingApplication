using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.ProductDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Products;

namespace MealsOrderingApplication.Services.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository, IProductFilter<Product>
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

        public virtual async Task<IQueryable<Product>> FilterByNameAsync(IQueryable<Product> products, string name)
        {
            return await Task.FromResult(products.Where(p => p.Name.Contains(name)));
        }

        public virtual async Task<IQueryable<Product>> FilterByCategoryAsync(IQueryable<Product> products, int categoryId)
        {
            return await Task.FromResult(products.Where(p => p.CategoryId == categoryId));
        }

        public virtual async Task<IQueryable<Product>> FilterByPriceAsync(IQueryable<Product> products, double? minPrice, double? maxPrice)
        {
            if (minPrice is null || minPrice <= 0) minPrice = 1;
            if (maxPrice is null) maxPrice = double.MaxValue;

            if (minPrice > maxPrice)
            {
                double? tmp = minPrice;
                maxPrice = minPrice;
                minPrice = tmp;
            }

            return await Task.FromResult(products.Where(p => p.Price >= minPrice && p.Price <= maxPrice));
        }
    }
}
