using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Demo_T2.Models
{
    public class UserDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public String FirstName { get; set; }

        [StringLength(50)]
        public String LastName { get; set; }

        [StringLength(15)]
        public String PhoneNumber { get; set; }

        [StringLength(255)]
        public String Address { get; set; }

        [ForeignKey("User")]
        [StringLength(50)]
        public String IdUser { get; set; }
        public User User { get; set; }


    }
}
