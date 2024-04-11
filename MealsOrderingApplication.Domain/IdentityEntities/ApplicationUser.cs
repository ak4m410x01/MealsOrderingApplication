using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
