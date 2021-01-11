// Author: Kaan Çembertaş 
// No: 200001684

using Microsoft.EntityFrameworkCore.Migrations;

namespace ImHere.DataAccess.Migrations
{
    public partial class addAttendenceImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendenceImages",
                columns: table => new
                {
                    lectureCode = table.Column<string>(maxLength: 6, nullable: false),
                    week = table.Column<int>(nullable: false),
                    image_url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendenceImages", x => new { x.lectureCode, x.week });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendenceImages");
        }
    }
}
