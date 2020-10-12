using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task CreateUser(User user)
        {
            await userRepository.CreateUser(user);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetAllUsers();
        }
    }
}
