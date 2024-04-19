using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Products;

namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Meals
{
    public interface IMealFilter : IProductFilter<Meal>
    {
    }
}
