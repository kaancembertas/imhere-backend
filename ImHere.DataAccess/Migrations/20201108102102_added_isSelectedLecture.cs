using Microsoft.EntityFrameworkCore.Migrations;

namespace ImHere.DataAccess.Migrations
{
    public partial class added_isSelectedLecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSelectedLecture",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSelectedLecture",
                table: "Users");
        }
    }
}
