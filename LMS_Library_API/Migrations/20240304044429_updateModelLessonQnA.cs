using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelLessonQnA : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_StudentNotificationSetting_StudentNotificationFeatures_StudentNotificationFeaturesId",
                table: "StudentNotificationSetting");

            migrationBuilder.DropIndex(
                name: "IX_StudentNotificationSetting_StudentNotificationFeaturesId",
                table: "StudentNotificationSetting");

            migrationBuilder.DropColumn(
                name: "StudentNotificationFeaturesId",
                table: "StudentNotificationSetting");

            migrationBuilder.AlterColumn<Guid>(
                name: "teacherId",
                table: "LessonQuestion",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "studentId",
                table: "LessonQuestion",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "teacherId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "studentId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNotificationSetting_featuresId",
                table: "StudentNotificationSetting",
                column: "featuresId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentNotificationSetting_StudentNotificationFeatures_featuresId",
                table: "StudentNotificationSetting",
                column: "featuresId",
                principalTable: "StudentNotificationFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_StudentNotificationSetting_StudentNotificationFeatures_featuresId",
                table: "StudentNotificationSetting");

            migrationBuilder.DropIndex(
                name: "IX_StudentNotificationSetting_featuresId",
                table: "StudentNotificationSetting");

            migrationBuilder.AddColumn<int>(
                name: "StudentNotificationFeaturesId",
                table: "StudentNotificationSetting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "teacherId",
                table: "LessonQuestion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "studentId",
                table: "LessonQuestion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "teacherId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "studentId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentNotificationSetting_StudentNotificationFeaturesId",
                table: "StudentNotificationSetting",
                column: "StudentNotificationFeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswer_User_teacherId",
                table: "LessonAnswer",
                column: "teacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuestion_User_teacherId",
                table: "LessonQuestion",
                column: "teacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentNotificationSetting_StudentNotificationFeatures_StudentNotificationFeaturesId",
                table: "StudentNotificationSetting",
                column: "StudentNotificationFeaturesId",
                principalTable: "StudentNotificationFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
