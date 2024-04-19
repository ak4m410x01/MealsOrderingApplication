using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Orders;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>, IOrdersFilter
    {
    }
}
