using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.OrderDTO
{
    public class UpdateOrderDTO : BaseOrderDTO, IUpdateDTO
    {
        public new List<int>? Quantities { get; set; }
        public new List<int>? ProductsId { get; set; }
    }
}
