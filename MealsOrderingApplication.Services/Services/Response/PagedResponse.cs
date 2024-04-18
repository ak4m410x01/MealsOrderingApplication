using MealsOrderingApplication.Domain.Interfaces.Extensions;
using MealsOrderingApplication.Services.IServices.Response;
using Microsoft.AspNetCore.Http;

namespace MealsOrderingApplication.Services.Services.Response
{
    public class PagedResponse<T> : Response<T>, IPagedResponse<T>
    {
        public PagedResponse(IQueryable<T> result, HttpRequest request, int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;

            TotalRecords = result.Count();
            TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);

            Result = result.PaginatePage(pageNumber, pageSize);

            Uri baseUrl = new($"{request.Scheme}://{request.Host}{request.Path}");

            FirstPage = new Uri($"{baseUrl}?pageNumber=1&pageSize={PageSize}");
            LastPage = new Uri($"{baseUrl}?pageNumber={TotalPages}&pageSize={PageSize}");

            PreviousPage = pageNumber <= 1 ? null : new Uri($"{baseUrl}?pageNumber={PageNumber - 1}&pageSize={PageSize}");
            NextPage = pageNumber >= TotalPages ? null : new Uri($"{baseUrl}?pageNumber={PageNumber + 1}&pageSize={PageSize}");

            Succeeded = true;
            Message = string.Empty;
            Errors = null;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? LastPage { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? PreviousPage { get; set; }
        public new IQueryable<T>? Result { get; set; }
    }
}
