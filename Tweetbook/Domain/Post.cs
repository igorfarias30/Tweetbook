namespace Tweetbook.Domain
{
    public class Post
    {
        public Post(long id)
            => Id = id;

        public long Id { get; set; }
    }
}
