using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.ProductDTO
{
    public class ProductDTODetails : BaseProductDTO, IDetailsDTO
    {
        public int Id { get; set; }
    }
}
