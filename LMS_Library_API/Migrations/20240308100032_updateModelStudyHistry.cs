using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelStudyHistry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyHistory_Document_documentId",
                table: "StudyHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyHistory_Student_studentId",
                table: "StudyHistory");

            migrationBuilder.RenameColumn(
                name: "watchMinutes",
                table: "StudyHistory",
                newName: "WatchMinutes");

            migrationBuilder.RenameColumn(
                name: "dateUpdate",
                table: "StudyHistory",
                newName: "DateUpdate");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "StudyHistory",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "documentId",
                table: "StudyHistory",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyHistory_documentId",
                table: "StudyHistory",
                newName: "IX_StudyHistory_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyHistory_Lesson_LessonId",
                table: "StudyHistory",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyHistory_Student_StudentId",
                table: "StudyHistory",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyHistory_Lesson_LessonId",
                table: "StudyHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyHistory_Student_StudentId",
                table: "StudyHistory");

            migrationBuilder.RenameColumn(
                name: "WatchMinutes",
                table: "StudyHistory",
                newName: "watchMinutes");

            migrationBuilder.RenameColumn(
                name: "DateUpdate",
                table: "StudyHistory",
                newName: "dateUpdate");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudyHistory",
                newName: "studentId");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "StudyHistory",
                newName: "documentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyHistory_LessonId",
                table: "StudyHistory",
                newName: "IX_StudyHistory_documentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyHistory_Document_documentId",
                table: "StudyHistory",
                column: "documentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyHistory_Student_studentId",
                table: "StudyHistory",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
