using System.Collections.Generic;

namespace Tweetbook.Contracts.v1.Response
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
