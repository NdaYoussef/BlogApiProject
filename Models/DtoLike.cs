namespace BlogAPI.Models
{
    public class PostLike
    {
        public int PostId { get; set; }
        
        public int userId { get; set; }
        public bool IsLike { get; set; }

    }
    public class CommentLike
    {
        public int CommentId { get; set; }

        public int userId { get; set; }
        public bool IsLike { get; set; }

    }
}
