using MealsOrderingApplication.Domain.IdentityEntities;

namespace MealsOrderingApplication.Domain.Entities
{
    public class Driver : ApplicationUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
