using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.IdentityEntities;
using MealsOrderingApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MealsOrderingApplication.Services.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context, UserManager<Customer> userManager) : base(context)
        {
            _userManager = userManager;
        }

        private readonly UserManager<Customer> _userManager;

        public override void Add(Customer entity)
        {
            base.Add(entity);
            _userManager.AddToRoleAsync(entity, "User");
        }
        public override async Task AddAsync(Customer entity)
        {
            await base.AddAsync(entity);
            await _userManager.AddToRoleAsync(entity, "User");
        }

        public override void Delete(Customer entity)
        {
            ApplicationUser? user = _context.Set<ApplicationUser>().Find(entity.Id);
            if (user is null)
                throw new NullReferenceException("Can't Find User");

            _context.Set<ApplicationUser>().Remove(user);
        }
        public override async Task DeleteAsync(Customer entity)
        {
            ApplicationUser? user = await _context.Set<ApplicationUser>().FindAsync(entity.Id);
            if (user is null)
                throw new NullReferenceException("Can't Find User");

            await Task.FromResult(_context.Set<ApplicationUser>().Remove(user));
        }
    }
}
