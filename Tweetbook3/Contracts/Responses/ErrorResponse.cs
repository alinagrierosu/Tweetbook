using System.Collections.Generic;

namespace Tweetbook3.Contracts.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
