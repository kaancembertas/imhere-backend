using ImHere.DataAccess.Abstract;
using ImHere.Entities;
using ImHere.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImHere.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        public async Task CreateUser(User user)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                imHereDbContext.Users.Add(user);
                await imHereDbContext.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                var user = await imHereDbContext.Users.FirstOrDefaultAsync(
                    u => u.email == email);
                return user;
            }
        }

        public async Task<UserInfoDto> GetUserInfoById(int id)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                User user = await imHereDbContext.Users.FirstOrDefaultAsync(u => u.id == id);
                return new UserInfoDto(user);
            }
        }

        public async Task<User> GetUserByNo(string no)
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.Users.FirstOrDefaultAsync(u => u.no == no && u.no != null);
            }
        }
    }
}
