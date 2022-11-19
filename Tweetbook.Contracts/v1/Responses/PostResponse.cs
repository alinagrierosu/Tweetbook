using System.Collections.Generic;

namespace Tweetbook.Contracts.Responses
{
    public class PostResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
        public IEnumerable<TagResponse> Tags { get; set; }
    }
}
