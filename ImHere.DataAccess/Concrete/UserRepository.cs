using ImHere.DataAccess.Abstract;
using ImHere.Entities;
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

        public async Task<List<User>> GetAllUsers()
        {
            using (var imHereDbContext = new ImHereDbContext())
            {
                return await imHereDbContext.Users.ToListAsync();
            }
        }
    }
}
