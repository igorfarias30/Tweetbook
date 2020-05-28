using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAll()
            => Ok(await _postService.GetPostsAsync());

        [HttpGet(Posts.Get)]
        [ProducesResponseType(200, Type = typeof(PostResponse))]
        public async Task<IActionResult> GetById(GetPostByIdRequest request)
            => Ok(await _postService.GetPostByIdAsync(request.Id));

        [HttpPost(Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {

            if (request?.Name == null)
                return BadRequest(new BadRequestResult());


            var post = new Post(request.Name);
            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse
            {
                Id = post.Id,
                Name = post.Name
            };
            return Created(locationUrl, response);
        }

        [HttpPut(Posts.Update)]
        public async Task<IActionResult> Update(UpdatePostRequest request)
        {
            var post = new Post { Id = request.Id, Name = request.Name };
            var updatedPost = await _postService.UpdatePostAsync(post);

            if (updatedPost)
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(Posts.Delete)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeletePost([FromRoute] DeletePostRequest request)
        {
            if (request?.Id == null)
                return BadRequest(new BadRequestResult());

            var deletedPost = await _postService.DeletePostAsync(request.Id);
            if (deletedPost)
                return Ok();

            return NotFound();
        }
    }
}