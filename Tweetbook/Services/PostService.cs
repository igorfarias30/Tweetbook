using System.Collections.Generic;
using System.Linq;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostService : IPostService
    {
        private readonly List<Post> _posts;

        public PostService()
        {
            _posts = new List<Post>();
            for (long i = 0; i < 10; i++)
            {
                _posts.Add(new Post
                {
                    Id = i,
                    Name = $"Post Name {i}"
                });
            }
        }

        public List<Post> GetPosts()
            => _posts;

        public Post GetPostById(long Id)
            => _posts.FirstOrDefault(x => x.Id == Id);

        public bool UpdatePost(Post postToUpdate)
        {
            var exists = GetPostById(postToUpdate.Id);

            if (exists == null)
                return false;

            var index = _posts.FindIndex(x => x.Id == postToUpdate.Id);
            _posts[index] = postToUpdate;
            return true;
        }

        public bool DeletePost(long postId)
        {
            if (GetPostById(postId) == null)
                return false;

            var post = _posts.Find(x => x.Id == postId);
            _posts.Remove(post);
            return true;
        }
    }
}