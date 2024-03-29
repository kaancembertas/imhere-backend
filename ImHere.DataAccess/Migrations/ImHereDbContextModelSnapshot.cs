﻿// <auto-generated />
using System;
using ImHere.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImHere.DataAccess.Migrations
{
    [DbContext(typeof(ImHereDbContext))]
    partial class ImHereDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImHere.Entities.Attendence", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("lecture_code")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<int>("week")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("user_id", "lecture_code", "week");

                    b.ToTable("Attendences");
                });

            modelBuilder.Entity("ImHere.Entities.AttendenceImage", b =>
                {
                    b.Property<string>("lectureCode")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<int>("week")
                        .HasColumnType("int");

                    b.Property<string>("image_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("lectureCode", "week");

                    b.ToTable("AttendenceImages");
                });

            modelBuilder.Entity("ImHere.Entities.FaceInfo", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("face_encoding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("FaceInfos");
                });

            modelBuilder.Entity("ImHere.Entities.Lecture", b =>
                {
                    b.Property<string>("code")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<int>("instructor_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("start_date")
                        .HasColumnType("datetime2");

                    b.HasKey("code");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("ImHere.Entities.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("image_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isSelectedLecture")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("no")
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<int>("role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("0");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ImHere.Entities.UserLecture", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("lecture_code")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.HasKey("user_id", "lecture_code");

                    b.ToTable("UserLectures");
                });
#pragma warning restore 612, 618
        }
    }
}
