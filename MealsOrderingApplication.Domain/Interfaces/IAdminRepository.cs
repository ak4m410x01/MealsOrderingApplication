using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Admins;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IAdminRepository : IApplicationUserRepository<Admin>, IAdminFilter
    {
    }
}
