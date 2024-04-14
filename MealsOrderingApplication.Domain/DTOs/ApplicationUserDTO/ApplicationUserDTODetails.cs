using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.ApplicationUserDTO
{
    public class ApplicationUserDTODetails : BaseApplicationUserDTO, IDetailsDTO
    {
        public string Id { get; set; } = default!;
    }
}
