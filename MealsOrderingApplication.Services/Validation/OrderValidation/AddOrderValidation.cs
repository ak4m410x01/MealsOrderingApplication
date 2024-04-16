using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;

namespace MealsOrderingApplication.Services.Validation.OrderValidation
{
    public class AddOrderValidation(IUnitOfWork unitOfWork) : BaseOrderValidation(unitOfWork), IAddOrderValidation
    {
        public async Task<string> AddIsValidAsync<TDto>(TDto dto) where TDto : IAddDTO
        {
            if (dto is AddOrderDTO addDto)
            {
                if (addDto.ProductsId.Count != addDto.Quantities.Count)
                    return "Quantities and ProductsId lists must have the same length.";

                if (addDto.ProductsId.Count <= 0)
                    return "Must be at least one item in ProductsId";


                for (int i = 0; i < addDto.ProductsId.Count; i++)
                {
                    if (addDto.ProductsId[i] <= 0)
                        return $"ProductId must be greater than 0.";

                    if ((await _unitOfWork.Products.GetByIdAsync(addDto.ProductsId[i])) is null)
                        return $"No Products found with this Id = {addDto.ProductsId[i]}";
                }

                for (int i = 0; i < addDto.Quantities.Count; i++)
                {
                    if (addDto.Quantities[i] <= 0)
                        return $"Quantity must be greater than 0.";
                }
                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateOrderDTO.");
        }
    }
}
