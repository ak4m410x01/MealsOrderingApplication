using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Products;
namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Drinks
{
    public interface IDrinksFilter : IProductsFilter<Drink>
    {
    }
}
