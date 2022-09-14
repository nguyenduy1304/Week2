using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.EntityFrameworkCore;

namespace Demo_T2.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {

        private ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {

            //return context.User.FromSqlRaw("SELECT * FROM [dbo].[User]").Include(c => c.UserDetail).ToList();

            return context.User.Include(c => c.UserDetail).ToList();
        }

        public User GetUserByID(String id)
        {
            //return context.User.FromSqlRaw("SELECT * FROM [dbo].[User] WHERE Id='" + id + "'").Include(c => c.UserDetail).SingleOrDefault();

            return context.User.Include(c => c.UserDetail).SingleOrDefault(c=>c.Id.Equals(id));
        }

        public void InsertUser(User user)
        {
            //context.User.Add(user);
            context.Database.ExecuteSqlRaw("INSERT INTO [User] ([Id], [Username], [Email], [Password]) VALUES(N'"
                                   + user.Id + "',N'" + user.Username + "',N'" + user.Email + "',N'" + user.Password + "')");
        }

        public void DeleteUser(String userid)
        {
            //    context.Database.ExecuteSqlRaw("DELETE FROM [dbo].[User] WHERE Id='" + userid + "'");
            User user = context.User.Find(userid);
            context.User.Remove(user);
        }
        
        public void UpdateUser(User user)
        {
            context.User.Update(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
