using MealsOrderingApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Services
{
    public class DataSeedService : IDataSeedService
    {
        public IEnumerable<IdentityRole> LoadRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
            };
        }
    }
}
