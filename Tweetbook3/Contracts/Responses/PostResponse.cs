using System.Collections.Generic;
using Tweetbook3.Domain;

namespace Tweetbook3.Contracts.Responses
{
    public class PostResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
        public IEnumerable<TagResponse> Tags { get; set; }
    }
}
