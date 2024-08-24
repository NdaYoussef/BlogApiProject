using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class User : IdentityUser
    {
        
        public byte[]? ProfilePictuer { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Like> Like { get; set; } = new List<Like>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
