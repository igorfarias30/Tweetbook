using Microsoft.AspNetCore.Mvc;

namespace Tweetbook.Contracts.v1.Requests
{
    public class GetPostByIdRequest
    {
        [FromRoute]
        public long Id { get; set; }
    }
}
