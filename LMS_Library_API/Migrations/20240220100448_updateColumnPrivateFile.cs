using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateColumnPrivateFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "PrivateFile",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "PrivateFile");
        }
    }
}
