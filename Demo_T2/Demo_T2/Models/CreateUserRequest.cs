using System.ComponentModel.DataAnnotations;

namespace Demo_T2.Models
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(200)]
        public string Username { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
