// Author: Kaan Çembertaş 
// No: 200001684

using Microsoft.EntityFrameworkCore.Migrations;

namespace ImHere.DataAccess.Migrations
{
    public partial class addedfaceencoding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isSelectedLecture",
                table: "Users",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "FaceInfos",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    face_encoding = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceInfos", x => x.user_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaceInfos");

            migrationBuilder.AlterColumn<bool>(
                name: "isSelectedLecture",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
