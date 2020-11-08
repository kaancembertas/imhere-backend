using ImHere.Entities;
using ImHere.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<UserInfoDto> GetUserInfoById(int Id);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByNo(string no);
        Task SetIsSelectedLectures(int userId,bool isSelected);
    }
}
