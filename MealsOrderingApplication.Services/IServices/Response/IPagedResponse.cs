namespace MealsOrderingApplication.Services.IServices.Response
{
    public interface IPagedResponse<T> : IResponse<T>
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; set; }
        int TotalRecords { get; set; }
        Uri? FirstPage { get; set; }
        Uri? LastPage { get; set; }
        Uri? NextPage { get; set; }
        Uri? PreviousPage { get; set; }
        new IQueryable<T>? Result { get; set; }
    }
}
