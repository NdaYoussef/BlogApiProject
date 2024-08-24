using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]

        public bool RememberMe { get; set; }
    }
}
