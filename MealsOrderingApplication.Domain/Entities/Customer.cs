using MealsOrderingApplication.Domain.IdentityEntities;

namespace MealsOrderingApplication.Domain.Entities
{
    public class Customer : ApplicationUser
    {
        public string Location { get; set; } = default!;

        public IEnumerable<Order> Orders { get; set; }
    }
}
