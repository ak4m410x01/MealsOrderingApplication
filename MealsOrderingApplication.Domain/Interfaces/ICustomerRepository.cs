using MealsOrderingApplication.Domain.DTOs.Customer;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Models;

namespace MealsOrderingApplication.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        public Task<AuthanticationModel> AddAsync(AddCustomerDTO model);
    }
}
