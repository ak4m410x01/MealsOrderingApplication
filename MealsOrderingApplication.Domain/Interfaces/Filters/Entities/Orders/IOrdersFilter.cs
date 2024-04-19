using MealsOrderingApplication.Domain.Entities;

namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Orders
{
    public interface IOrdersFilter
    {
        Task<IQueryable<Order>> FilterByCustomerAsync(IQueryable<Order> orders, string customerId);
    }
}
