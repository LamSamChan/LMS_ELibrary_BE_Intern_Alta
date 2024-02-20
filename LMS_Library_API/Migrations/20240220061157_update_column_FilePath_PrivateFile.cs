using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class update_column_FilePath_PrivateFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "PrivateFile",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "PrivateFile",
                type: "nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
