using Swashbuckle.AspNetCore.Filters;
using System;
using Tweetbook.Contracts.Responses;

namespace Tweetbook3.SwaggerExamples.Responses
{
    public class PostResponseExample : IExamplesProvider<PostResponse>
    {
        public PostResponse GetExamples()
        {
            return new PostResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "New post response"
            };
        }
    }
}
