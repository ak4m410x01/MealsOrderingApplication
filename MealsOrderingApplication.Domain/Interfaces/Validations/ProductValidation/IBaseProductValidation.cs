namespace MealsOrderingApplication.Domain.Interfaces.Validations.ProductValidation
{
    public interface IBaseProductValidation : IBaseValidation
    {
        Task<bool> IsCategoryExistsAsync(int categoryId);
    }
}
