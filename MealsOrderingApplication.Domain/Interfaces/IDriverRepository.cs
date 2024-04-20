using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Drivers;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IDriverRepository : IApplicationUserRepository<Driver>, IDriverFilter
    {
    }
}
