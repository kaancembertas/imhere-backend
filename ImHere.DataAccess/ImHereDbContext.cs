using ImHere.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImHere.DataAccess
{
    public class ImHereDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=ImHereDb;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
    }
}
