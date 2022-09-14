using Demo_T2.Models;
using Demo_T2.Repository;
using Microsoft.EntityFrameworkCore;

namespace Demo_T2.DAL
{
    public class UserDetailRepository : IUserDetailRepository, IDisposable
    {
        private ApplicationDbContext context;

        public UserDetailRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserDetail> GetUserDetails()
        {
            return context.UserDetail.ToList();
        }

        public UserDetail GetUserDetailByID(String id)
        {
            return context.UserDetail.Find(id);
        }

        public void InsertUserDetail(UserDetail userDetail)
        {
            //context.Database.ExecuteSqlRaw("INSERT [dbo].[UserDetail] ([FirstName], [LastName], [PhoneNumber], [Address], [IdUser]) VALUES (N'" +
            //userDetail.FirstName + "',N'" + userDetail.LastName + "',N'" + userDetail.PhoneNumber + "',N'" + userDetail.Address + "',N'" + userDetail.IdUser + "')");

            context.UserDetail.Add(userDetail);
        }

        public void DeleteUserDetail(String userid)
        {
            UserDetail userDetail = context.UserDetail.Find(userid);
            context.UserDetail.Remove(userDetail);
        }

        public void UpdateUserDetail(UserDetail userDetail)
        {
            context.Entry(userDetail).State = EntityState.Modified;
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
