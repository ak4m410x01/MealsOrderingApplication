using MealsOrderingApplication.Domain.Entities;
namespace MealsOrderingApplication.Domain.Interfaces.Filters.Entities.Products
{
    public interface IProductsFilter<TProduct> where TProduct : Product
    {
        Task<IQueryable<TProduct>> FilterByNameAsync(IQueryable<TProduct> products, string name);
        Task<IQueryable<TProduct>> FilterByCategoryAsync(IQueryable<TProduct> products, int categoryId);
        Task<IQueryable<TProduct>> FilterByPriceAsync(IQueryable<TProduct> products, double minPrice = 0, double maxPrice = double.MaxValue);
    }
}
