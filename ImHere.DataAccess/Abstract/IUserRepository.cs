using ImHere.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<List<User>> GetAllUsers();
    }
}
