namespace Tweetbook.Domain
{
    public class Post
    {
        public Post(long id)
            => Id = id;

        public Post() { }

        public long Id { get; set; }
    }
}
