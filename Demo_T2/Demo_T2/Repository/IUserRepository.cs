using Demo_T2.Models;

namespace Demo_T2.Repository
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(String id);
        void InsertUser(User user);
        void DeleteUser(String userid);
        void UpdateUser(User user);
        void Save();
    }
}
