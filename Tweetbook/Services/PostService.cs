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
    }
}