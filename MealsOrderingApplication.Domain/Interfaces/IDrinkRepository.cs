using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Drinks;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IDrinkRepository : IBaseRepository<Drink>, IDrinkFilter
    {
    }
}
