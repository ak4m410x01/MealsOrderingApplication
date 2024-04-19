using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Customers;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface ICustomerRepository : IApplicationUserRepository<Customer>, ICustomerFilter
    {
    }
}
