using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public bool IsLike { get; set; }
        public DateTime CreatedDate { get; set; }
       
        public int UserId { get; set; }
        public User User { get; set; }
      
        public Post Post { get; set; }
        public int PostId { get; set; }
        public Comment Comment { get; set; }
        public int? CommentId { get; set; }
    }
}
