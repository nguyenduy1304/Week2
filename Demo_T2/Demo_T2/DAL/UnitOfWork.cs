using Demo_T2.Models;

namespace Demo_T2.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context;
        private GenericRepository<User> userRepository;
        private GenericRepository<UserDetail> userDetailRepository;

            public UnitOfWork(ApplicationDbContext context)
            {
                this.context = context;
            }
        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<UserDetail> UserDetailRepository
        {
            get
            {

                if (this.userDetailRepository == null)
                {
                    this.userDetailRepository = new GenericRepository<UserDetail>(context);
                }
                return userDetailRepository;
            }
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
