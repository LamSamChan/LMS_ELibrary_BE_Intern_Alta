using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_Student_StudentId",
                table: "NotificationClassStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationClassStudent",
                table: "NotificationClassStudent");

            migrationBuilder.DropIndex(
                name: "IX_NotificationClassStudent_StudentId",
                table: "NotificationClassStudent");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "NotificationClassStudent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationClassStudent",
                table: "NotificationClassStudent",
                columns: new[] { "SubjectNotificationId", "ClassId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationClassStudent",
                table: "NotificationClassStudent");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "NotificationClassStudent",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationClassStudent",
                table: "NotificationClassStudent",
                columns: new[] { "SubjectNotificationId", "ClassId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationClassStudent_StudentId",
                table: "NotificationClassStudent",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_Student_StudentId",
                table: "NotificationClassStudent",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
