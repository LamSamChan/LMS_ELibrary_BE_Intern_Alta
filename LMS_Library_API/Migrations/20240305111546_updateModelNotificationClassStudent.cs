using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelNotificationClassStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_Class_classId",
                table: "NotificationClassStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_Student_studentId",
                table: "NotificationClassStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_SubjectNotification_subjectNotificationId",
                table: "NotificationClassStudent");

            migrationBuilder.RenameColumn(
                name: "isForAllStudent",
                table: "NotificationClassStudent",
                newName: "IsForAllStudent");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "NotificationClassStudent",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "classId",
                table: "NotificationClassStudent",
                newName: "ClassId");

            migrationBuilder.RenameColumn(
                name: "subjectNotificationId",
                table: "NotificationClassStudent",
                newName: "SubjectNotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationClassStudent_studentId",
                table: "NotificationClassStudent",
                newName: "IX_NotificationClassStudent_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationClassStudent_classId",
                table: "NotificationClassStudent",
                newName: "IX_NotificationClassStudent_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_Class_ClassId",
                table: "NotificationClassStudent",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_Student_StudentId",
                table: "NotificationClassStudent",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_SubjectNotification_SubjectNotificationId",
                table: "NotificationClassStudent",
                column: "SubjectNotificationId",
                principalTable: "SubjectNotification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_Class_ClassId",
                table: "NotificationClassStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_Student_StudentId",
                table: "NotificationClassStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationClassStudent_SubjectNotification_SubjectNotificationId",
                table: "NotificationClassStudent");

            migrationBuilder.RenameColumn(
                name: "IsForAllStudent",
                table: "NotificationClassStudent",
                newName: "isForAllStudent");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "NotificationClassStudent",
                newName: "studentId");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "NotificationClassStudent",
                newName: "classId");

            migrationBuilder.RenameColumn(
                name: "SubjectNotificationId",
                table: "NotificationClassStudent",
                newName: "subjectNotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationClassStudent_StudentId",
                table: "NotificationClassStudent",
                newName: "IX_NotificationClassStudent_studentId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationClassStudent_ClassId",
                table: "NotificationClassStudent",
                newName: "IX_NotificationClassStudent_classId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_Class_classId",
                table: "NotificationClassStudent",
                column: "classId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_Student_studentId",
                table: "NotificationClassStudent",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationClassStudent_SubjectNotification_subjectNotificationId",
                table: "NotificationClassStudent",
                column: "subjectNotificationId",
                principalTable: "SubjectNotification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
