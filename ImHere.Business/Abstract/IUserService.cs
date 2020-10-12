using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.Business.Abstract
{
    public interface IUserService
    {
        public Task CreateUser(User user);
        public Task<List<User>> GetAllUsers();
    }
}
