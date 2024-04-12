using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.IServices
{
    public interface IDataSeedService
    {
        public IEnumerable<IdentityRole> LoadRoles();
    }
}
