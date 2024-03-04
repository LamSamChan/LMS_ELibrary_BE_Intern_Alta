using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelStudyTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyTime",
                table: "StudyTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyTime",
                table: "StudyTime",
                columns: new[] { "studentId", "subjectId", "studyDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyTime",
                table: "StudyTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyTime",
                table: "StudyTime",
                columns: new[] { "studentId", "subjectId" });
        }
    }
}
