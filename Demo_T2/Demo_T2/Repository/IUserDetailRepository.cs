using Demo_T2.Models;

namespace Demo_T2.Repository
{
    public interface IUserDetailRepository : IDisposable
    {
        IEnumerable<UserDetail> GetUserDetails();
        UserDetail GetUserDetailByID(String id);
        void InsertUserDetail(UserDetail userDetail);
        void DeleteUserDetail(String userid);
        void UpdateUserDetail(UserDetail userDetail);
        void Save();

    }
}
