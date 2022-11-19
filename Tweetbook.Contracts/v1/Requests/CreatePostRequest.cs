using System.Collections.Generic;
using Tweetbook.Contracts.Requests;

namespace Tweetbook.Contracts.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; }
        public IEnumerable<TagRequest> Tags { get; set; }
    }
}
