// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Business.Abstract;
using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace ImHere.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            user.role = 0;
            user.password = BC.HashPassword(user.password);
            user.email = user.email.ToLower();
            await _userRepository.CreateUser(user);
        }

        public async Task<UserInfoDto> GetUserInfoById(int id)
        {
            return await _userRepository.GetUserInfoById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email.ToLower());
        }

        public async Task<User> GetUserByNo(string no)
        {
            return await _userRepository.GetUserByNo(no);
        }

        public async Task<bool> IsEmailExists(string email)
        {
            var user = await GetUserByEmail(email);
            return user != null;
        }

        public async Task<bool> IsNoExists(string no)
        {
            var user = await GetUserByNo(no);
            return user != null;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<bool> IsUserExists(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return user != null;
        }
    }
}
