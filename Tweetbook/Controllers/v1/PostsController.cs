using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.v1.Requests;
using Tweetbook.Contracts.v1.Response;
using Tweetbook.Domain;
using Tweetbook.Services;
using static Tweetbook.Contracts.v1.ApiRoutes;

namespace Tweetbook.Controllers.v1
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
            => _postService = postService;

        [HttpGet(Posts.GetAll)]
        [ProducesResponseType(200, Type = typeof(PostResponse[]))]
        public IActionResult GetAll()
            => Ok(_postService.GetPosts());

        [HttpGet(Posts.Get)]
        [ProducesResponseType(200, Type = typeof(PostResponse))]
        public IActionResult GetById([FromRoute] long postId)
            => Ok(_postService.GetPostById(postId));

        [HttpPost(Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest request)
        {

            if (request?.Id == null)
                return BadRequest(new BadRequestResult());


            var post = new Post(request.Id, request.Name);
            _postService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(locationUrl, response);
        }

        [HttpPut(Posts.Update)]
        public IActionResult Update([FromBody] UpdatePostRequest request)
        {
            var post = new Post { Id = request.Id, Name = request.Name };
            var updatedPost = _postService.UpdatePost(post);

            if (updatedPost)
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(Posts.Delete)]
        [ProducesResponseType(200)]
        public IActionResult DeletePost([FromRoute] DeletePostRequest request)
        {
            if (request?.Id == null)
                return BadRequest(new BadRequestResult());

            var deletedPost = _postService.DeletePost(request.Id);
            if (deletedPost)
                return Ok();

            return NotFound();
        }
    }
}