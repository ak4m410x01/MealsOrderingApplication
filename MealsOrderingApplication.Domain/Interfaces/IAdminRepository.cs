using MealsOrderingApplication.Domain.DTOs.Admin;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Models;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        public Task<AuthanticationModel> AddAsync(AddAdminDTO model);
    }
}
