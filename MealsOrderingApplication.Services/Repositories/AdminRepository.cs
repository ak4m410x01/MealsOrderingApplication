using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context, UserManager<Admin> userManager) : base(context)
        {
            _userManager = userManager;
        }

        private readonly UserManager<Admin> _userManager;

        public override void Add(Admin entity)
        {
            base.Add(entity);
            _userManager.AddToRoleAsync(entity, "User");
        }
        public override async Task AddAsync(Admin entity)
        {
            await base.AddAsync(entity);
            await _userManager.AddToRoleAsync(entity, "User");
        }

        public override void Delete(Admin entity)
        {
            ApplicationUser? user = _context.Set<ApplicationUser>().Find(entity.Id);
            if (user is null)
                throw new NullReferenceException("Can't Find User");

            _context.Set<ApplicationUser>().Remove(user);
        }
        public override async Task DeleteAsync(Admin entity)
        {
            ApplicationUser? user = await _context.Set<ApplicationUser>().FindAsync(entity.Id);
            if (user is null)
                throw new NullReferenceException("Can't Find User");

            await Task.FromResult(_context.Set<ApplicationUser>().Remove(user));
        }
    }
}
