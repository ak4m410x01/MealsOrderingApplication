using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.DriverDTO
{
    public class DriverDTODetails : BaseDriverDTO, IDetailsDTO
    {
        public string Id { get; set; } = default!;
    }
}
