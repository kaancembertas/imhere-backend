// Author: Kaan Çembertaş 
// No: 200001684

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImHere.DataAccess.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendences",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    lecture_code = table.Column<string>(maxLength: 6, nullable: false),
                    week = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendences", x => new { x.user_id, x.lecture_code, x.week });
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    code = table.Column<string>(maxLength: 6, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    instructor_id = table.Column<int>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "UserLectures",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    lecture_code = table.Column<string>(maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLectures", x => new { x.user_id, x.lecture_code });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    no = table.Column<string>(maxLength: 9, nullable: true),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    surname = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 60, nullable: false),
                    role = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    image_url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendences");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "UserLectures");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
