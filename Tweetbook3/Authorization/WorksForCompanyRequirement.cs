using Microsoft.AspNetCore.Authorization;

namespace Tweetbook3.Authorization
{
    public class WorksForCompanyRequirement : IAuthorizationRequirement
    {
        public string DomaninName { get;  }

        public WorksForCompanyRequirement(string domaninName)
        {
            DomaninName = domaninName;
        }
    }
}
