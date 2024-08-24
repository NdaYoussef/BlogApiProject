using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class DtoNewUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        
    }
}
