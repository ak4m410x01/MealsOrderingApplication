namespace MealsOrderingApplication.Domain.Interfaces.Extensions
{
    public static class Pagination
    {
        public static IQueryable<T> PaginatePage<T>(this IQueryable<T> TSource, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            return TSource.Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize);
        }
    }
}
