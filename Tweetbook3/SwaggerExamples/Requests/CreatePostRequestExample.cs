using Swashbuckle.AspNetCore.Filters;
using Tweetbook.Contracts.Requests;

namespace Tweetbook3.SwaggerExamples.Requests
{
    public class CreatePostRequestExample : IExamplesProvider<CreatePostRequest>
    {
        public CreatePostRequest GetExamples()
        {
            return new CreatePostRequest
            {
                Name = "New post"
            };
        }
    }
}
