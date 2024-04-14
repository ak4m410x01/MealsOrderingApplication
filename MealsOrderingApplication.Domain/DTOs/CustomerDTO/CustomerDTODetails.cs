using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.CustomerDTO
{
    public class CustomerDTODetails : BaseCustomerDTO, IDetailsDTO
    {
        public string Id { get; set; } = default!;
    }
}
