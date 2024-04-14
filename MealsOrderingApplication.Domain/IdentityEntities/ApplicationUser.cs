using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
