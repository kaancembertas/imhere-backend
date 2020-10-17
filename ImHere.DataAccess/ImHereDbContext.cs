﻿using ImHere.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.role)
                .HasDefaultValueSql("0");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
    }
}