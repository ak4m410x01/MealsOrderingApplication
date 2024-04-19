using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Meals;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IMealRepository : IBaseRepository<Meal>, IMealFilter
    {
    }
}
