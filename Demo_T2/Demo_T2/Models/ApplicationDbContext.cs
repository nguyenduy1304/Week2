using Microsoft.EntityFrameworkCore;

namespace Demo_T2.Models
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<User> User { get; set; }
            public DbSet<UserDetail> User_Detail { get; set; }
    }
}
