using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using mptc.dgc.sample.application.DTOs.Success;

namespace mptc.dgc.sample.application.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<ResponsePagingDto<TResult>> ToPagedResultAsync<TSource, TResult>(
            this IQueryable<TSource> query,
            int skip,
            int top,
            Func<TSource, TResult> selector,
            HttpContext httpContext,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(skip)
                .Take(top)
                .ToListAsync(cancellationToken);
            var mappedItems = items.Select(selector).ToList();

            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}";

            return new ResponsePagingDto<TResult>
            {
                Value = mappedItems,
                TotalCount = totalCount,
                NextLink = totalCount > skip + top ? $"{baseUrl}?skip={skip + top}&top={top}" : null
            };
        }
    }
}