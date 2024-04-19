using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Categories;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>, ICategoriesFilter
    {
    }
}
