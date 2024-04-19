using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.ApplicationUsers;
namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Admins
{
    public interface IAdminFilter : IApplicationUserFilter<Admin>
    {
    }
}
