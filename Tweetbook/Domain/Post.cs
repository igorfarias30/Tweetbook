namespace Tweetbook.Domain
{
    public class Post
    {
        public Post(long id)
            => Id = id;

        public Post(long id, string name) : this(id) => Name = name;

        public Post() { }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}
