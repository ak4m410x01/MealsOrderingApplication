using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.AdminDTO
{
    public class AdminDTODetails : BaseAdminDTO, IDetailsDTO
    {
        public string Id { get; set; } = default!;
    }
}
