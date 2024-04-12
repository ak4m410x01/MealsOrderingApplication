namespace MealsOrderingApplication.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;

        public IEnumerable<Order> Orders { get; set; }
    }
}
