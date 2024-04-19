using MealsOrderingApplication.Domain.IdentityEntities;
namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.ApplicationUsers
{
    public interface IApplicationUserFilter<TApplicationUser> where TApplicationUser : ApplicationUser
    {
        Task<IQueryable<TApplicationUser>> FilterByEmailAsync(IQueryable<TApplicationUser> users, string email);
        Task<IQueryable<TApplicationUser>> FilterByUsernameAsync(IQueryable<TApplicationUser> users, string username);
        Task<IQueryable<TApplicationUser>> FilterByNameAsync(IQueryable<TApplicationUser> users, string name);
    }
}
