namespace MealsOrderingApplication.Domain.DTOs.Customer
{
    public class UpdateCustomerDTO : BaseCustomerDTO
    {
        public new string? FirstName { get; set; }
        public new string? LastName { get; set; }

        public new string? Email { get; set; }
        public new string? Username { get; set; }
        public new string? Password { get; set; }

        public new string? Phone { get; set; }
        public new string? Location { get; set; }
    }
}
