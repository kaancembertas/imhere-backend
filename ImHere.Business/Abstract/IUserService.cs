using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IUserService
    {
        public Task CreateUser(User user);
        public Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
        public Task<UserInfoDto> GetUserInfoById(int id);
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserByNo(string no);
        public Task<bool> IsEmailExists(string email);
        public Task<bool> IsNoExists(string no);
    }
}
