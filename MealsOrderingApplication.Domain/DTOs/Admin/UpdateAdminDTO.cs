using System.ComponentModel.DataAnnotations;

namespace MealsOrderingApplication.Domain.DTOs.Admin
{
    public class UpdateAdminDTO : BaseAdminDTO
    {
        public new string? FirstName { get; set; }
        public new string? LastName { get; set; }

        public new string? Email { get; set; }
        public new string? Username { get; set; }
        public new string? Password { get; set; }
    }
}
