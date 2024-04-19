using MealsOrderingApplication.Domain.Entities;

namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Categories
{
    public interface ICategoriesFilter
    {
        Task<IQueryable<Category>> FilterByNameAsync(IQueryable<Category> categories, string name);
    }
}
