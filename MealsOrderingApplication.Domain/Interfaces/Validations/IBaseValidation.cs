namespace MealsOrderingApplication.Domain.Interfaces.Validations
{
    public interface IBaseValidation<T> where T : class
    {
        string IsValid(T entity);
    }
}
