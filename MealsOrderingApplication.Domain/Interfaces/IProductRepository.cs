using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Products;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>, IProductsFilter<Product>
    {
    }
}
