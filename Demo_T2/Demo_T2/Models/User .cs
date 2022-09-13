using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_T2.Models
{
    public class User
    {
        [Key]
        [StringLength(50)]
        public String Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public UserDetail UserDetail { get; set; }
    }
}
