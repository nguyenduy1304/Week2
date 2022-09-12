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
            return context.User.ToList();
        }

        public User GetUserByID(int id)
        {
            return context.User.Find(id);
        }

        public void InsertUser(User user)
        {
            context.User.Add(user);
        }

        public void DeleteUser(int userid)
        {
            User user = context.User.Find(userid);
            context.User.Remove(user);
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
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
