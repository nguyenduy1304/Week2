using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL
{
    public class User_Detail
    {
        [StringLength(50)]
        public int FirstName { get; set; }

        [StringLength(50)]
        public int LastName { get; set; }

        [StringLength(15)]
        public int PhoneNumber { get; set; }

        [StringLength(255)]
        public int Address { get; set; }

        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }


    }
}
