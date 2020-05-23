using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tweetbook.Contracts.v1.Requests;
using Tweetbook.Contracts.v1.Response;
using Tweetbook.Domain;
using static Tweetbook.Contracts.v1.ApiRoutes;

namespace Tweetbook.Controllers.v1
{
    public class PostsController : Controller
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();
            for (long i = 0; i < 10; i++)
            {
                _posts.Add(new Post { Id = i });
            }
        }

        [HttpGet(Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }

        [HttpPost(Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest request)
        {

            if (request?.Id == null)
            {
                return BadRequest(new BadRequestResult());
            }

            var post = new Post(request.Id);
            _posts.Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(locationUrl, post);
        }
    }
}