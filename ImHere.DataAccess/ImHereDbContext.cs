﻿// Author: Kaan Çembertaş 
// No: 200001684

using ImHere.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ImHere.DataAccess
{
    public class ImHereDbContext : DbContext
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

            modelBuilder.Entity<UserLecture>()
                .HasKey(ul => new { ul.user_id, ul.lecture_code });

            modelBuilder.Entity<Lecture>()
                .Property(l => l.code)
                .HasMaxLength(6);

            modelBuilder.Entity<Attendence>()
                .HasKey(a => new { a.user_id, a.lecture_code, a.week });

            modelBuilder.Entity<User>()
                .Property(u => u.isSelectedLecture)
                .HasDefaultValue(false);

            modelBuilder.Entity<AttendenceImage>()
                .HasKey(ai => new { ai.lectureCode, ai.week });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<UserLecture> UserLectures { get; set; }
        public DbSet<FaceInfo> FaceInfos { get; set; }
        public DbSet<AttendenceImage> AttendenceImages { get; set; }
    }
}
