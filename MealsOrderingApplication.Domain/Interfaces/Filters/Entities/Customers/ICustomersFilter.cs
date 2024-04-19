using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.ApplicationUsers;
namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Customers
{
    public interface ICustomerFilter : IApplicationUserFilter<Customer>
    {
    }
}
