using Cosmonaut;
using Cosmonaut.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class CosmosPostService : IPostService
    {
        private readonly ICosmosStore<CosmosPostDto> _cosmosStore;

        public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
            => _cosmosStore = cosmosStore;

        public async Task<bool> CreatePostAsync(Post post)
        {
            var cosmosPost = new CosmosPostDto
            {
                Id = post.Id.ToString(),
                Name = post.Name
            };

            var response = await _cosmosStore.AddAsync(cosmosPost);
            return response.IsSuccess;

        }

        public async Task<bool> DeletePostAsync(long postId)
        {
            var response = await _cosmosStore.RemoveByIdAsync(postId.ToString());
            return response.IsSuccess;
        }

        public async Task<Post> GetPostByIdAsync(long Id)
        {
            var response = await _cosmosStore.FindAsync(Id.ToString());

            if (response == null)
                return null;

            return new Post { Id = long.Parse(response.Id), Name = response.Name };
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts.Select(x => new Post { Id = long.Parse(x.Id), Name = x.Name }).ToList();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            var newPost = new CosmosPostDto
            {
                Id = postToUpdate.Id.ToString(),
                Name = postToUpdate.Name
            };

            var response = await _cosmosStore.UpdateAsync(newPost);
            return response.IsSuccess;
        }
    }
}
