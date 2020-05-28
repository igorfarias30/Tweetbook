using System.Collections.Generic;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        List<Post> GetPosts();
        Post GetPostById(long Id);
        bool UpdatePost(Post postToUpdate);
        bool DeletePost(long postId);
    }
}