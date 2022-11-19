using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tweetbook.Contracts.Responses;
using Tweetbook.Contracts.v1.Requests.Queries;
using Tweetbook.Contracts.v1.Responses;
using Tweetbook3.Domain;
using Tweetbook3.Services;

namespace Tweetbook.Contracts.Helpers
{
    public class PaginationHelpers
    {
        internal static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter pagination, IEnumerable<T> postResponses)
        {
            var nextPage = pagination.PageNumber >= 1
              ? uriService.GetAllPostsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
              : null;
            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService.GetAllPostsUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString()
                : null;

            var paginationResponse = new PagedResponse<T>
            {
                Data = postResponses,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = postResponses.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
            return paginationResponse;
        }
    }
}
