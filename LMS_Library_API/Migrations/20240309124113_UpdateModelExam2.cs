using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class UpdateModelExam2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exam_DepartmentId",
                table: "Exam");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "Document",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_DepartmentId",
                table: "Exam",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Exam_DepartmentId",
                table: "Exam");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "Document",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exam_DepartmentId",
                table: "Exam",
                column: "DepartmentId",
                unique: true);
        }
    }
}
