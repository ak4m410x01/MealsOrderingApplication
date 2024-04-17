using MealsOrderingApplication.Domain;
using MealsOrderingApplication.Domain.DTOs.OrderDTO;
using MealsOrderingApplication.Domain.Interfaces.DTOs;
using MealsOrderingApplication.Domain.Interfaces.Validations.OrderValidation;

namespace MealsOrderingApplication.Services.Validation.OrderValidation
{
    public class UpdateOrderValidation(IUnitOfWork unitOfWork) : BaseOrderValidation(unitOfWork), IUpdateOrderValidation
    {
        public async Task<string> UpdateIsValidAsync<TDto>(TDto dto) where TDto : IUpdateDTO
        {
            if (dto is UpdateOrderDTO updateDto)
            {
                if (updateDto.ProductsId is not null && updateDto.Quantities is not null)
                {
                    if (updateDto.ProductsId.Count != updateDto.Quantities.Count)
                        return "Quantities and ProductsId lists must have the same length.";

                    if (updateDto.ProductsId.Count <= 0)
                        return "Must be at least one item in ProductsId";


                    for (int i = 0; i < updateDto.ProductsId.Count; i++)
                    {
                        if (updateDto.ProductsId[i] <= 0)
                            return $"ProductId must be greater than 0.";

                        if (!(await IsProductExists(updateDto.ProductsId[i])))
                            return $"No Products found with this Id = {updateDto.ProductsId[i]}";
                    }

                    for (int i = 0; i < updateDto.Quantities.Count; i++)
                    {
                        if (updateDto.Quantities[i] <= 0)
                            return $"Quantity must be greater than 0.";
                    }
                }

                else if ((updateDto.ProductsId is null && updateDto.Quantities is not null) || (updateDto.ProductsId is not null && updateDto.Quantities is null))
                {
                    return "Quantities and ProductsId must be not nulls";
                }

                return string.Empty;
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateOrderDTO.");
        }
    }
}
