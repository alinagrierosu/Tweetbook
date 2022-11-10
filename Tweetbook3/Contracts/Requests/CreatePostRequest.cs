using System.Collections.Generic;
using Tweetbook3.Domain;

namespace Tweetbook3.Contracts.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; }
        public IEnumerable<TagRequest> Tags { get; set; }
    }
}
