using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonAnswer_User_teacherId",
                table: "LessonAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonQuestion_User_teacherId",
                table: "LessonQuestion");

            migrationBuilder.DropIndex(
                name: "IX_LessonQuestion_studentId",
                table: "LessonQuestion");

            migrationBuilder.DropIndex(
                name: "IX_LessonQuestion_teacherId",
                table: "LessonQuestion");

            migrationBuilder.DropIndex(
                name: "IX_LessonAnswer_studentId",
                table: "LessonAnswer");

            migrationBuilder.DropIndex(
                name: "IX_LessonAnswer_teacherId",
                table: "LessonAnswer");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "LessonQuestion");

            migrationBuilder.DropColumn(
                name: "teacherId",
                table: "LessonQuestion");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "LessonAnswer");

            migrationBuilder.DropColumn(
                name: "teacherId",
                table: "LessonAnswer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "studentId",
                table: "LessonQuestion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "teacherId",
                table: "LessonQuestion",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "studentId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "teacherId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuestion_studentId",
                table: "LessonQuestion",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuestion_teacherId",
                table: "LessonQuestion",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAnswer_studentId",
                table: "LessonAnswer",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAnswer_teacherId",
                table: "LessonAnswer",
                column: "teacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswer_User_teacherId",
                table: "LessonAnswer",
                column: "teacherId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuestion_User_teacherId",
                table: "LessonQuestion",
                column: "teacherId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
