using Demo_T2.Models;
using Demo_T2.Repository;
using static Demo_T2.Models.Hash;

namespace Demo_T2.Services
{
    interface IUserSevice
    {
        string CreateUser(CreateUserRequest createUserRequest);
    }

    public class UserService : IUserSevice
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string CreateUser(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = createUserRequest.Email,
                Password = Hash.GetHash(createUserRequest.Password, HashType.MD5)
                //TODO: other properties
            };

            _userRepository.InsertUser(user);

            _userRepository.Save();

            return user.Id;
        }
    }
}
