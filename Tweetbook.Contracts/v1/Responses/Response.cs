using System;
using System.Collections.Generic;
using System.Text;

namespace Tweetbook.Contracts.v1.Responses
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T response)
        {
            Data = response;
        }

        private T Data { get; set; }
    }
}
