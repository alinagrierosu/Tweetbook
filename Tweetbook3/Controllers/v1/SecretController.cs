using Microsoft.AspNetCore.Mvc;
using Tweetbook3.Filters;

namespace Tweetbook3.Controllers.v1
{
    [ApiKeyAuth]
    public class SecretController : ControllerBase
    {

        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            return Ok("I have no secrets");
        }
    }
}
