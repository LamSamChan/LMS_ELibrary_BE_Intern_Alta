using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentAccess_Class_classId",
                table: "DocumentAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonAccess_Class_classId",
                table: "LessonAccess");

            migrationBuilder.DropIndex(
                name: "IX_LessonAccess_classId",
                table: "LessonAccess");

            migrationBuilder.DropIndex(
                name: "IX_DocumentAccess_classId",
                table: "DocumentAccess");

            migrationBuilder.DropColumn(
                name: "classId",
                table: "LessonAccess");

            migrationBuilder.DropColumn(
                name: "classId",
                table: "DocumentAccess");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "classId",
                table: "LessonAccess",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "classId",
                table: "DocumentAccess",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAccess_classId",
                table: "LessonAccess",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAccess_classId",
                table: "DocumentAccess",
                column: "classId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentAccess_Class_classId",
                table: "DocumentAccess",
                column: "classId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAccess_Class_classId",
                table: "LessonAccess",
                column: "classId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
