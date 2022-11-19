using System;
using Tweetbook.Contracts.v1.Requests.Queries;

namespace Tweetbook3.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);
        Uri GetAllPostsUri(PaginationQuery pagination = null);
    }
}
