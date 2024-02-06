using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class _020624_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassSubject",
                columns: table => new
                {
                    classId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    subjectId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubject", x => new { x.classId, x.subjectId });
                    table.ForeignKey(
                        name: "FK_ClassSubject_Class_classId",
                        column: x => x.classId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassSubject_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    studentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subjectId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    subjectMark = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.studentId, x.subjectId });
                    table.ForeignKey(
                        name: "FK_StudentSubject_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyHistory",
                columns: table => new
                {
                    studentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    documentId = table.Column<int>(type: "int", nullable: false),
                    watchMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyHistory", x => new { x.studentId, x.documentId });
                    table.ForeignKey(
                        name: "FK_StudyHistory_Document_documentId",
                        column: x => x.documentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyHistory_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyTime",
                columns: table => new
                {
                    studentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subjectId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    studyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totalTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTime", x => new { x.studentId, x.subjectId });
                    table.ForeignKey(
                        name: "FK_StudyTime_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyTime_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherClass",
                columns: table => new
                {
                    teacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    classId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClass", x => new { x.classId, x.teacherId });
                    table.ForeignKey(
                        name: "FK_TeacherClass_Class_classId",
                        column: x => x.classId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClass_User_teacherId",
                        column: x => x.teacherId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubject_subjectId",
                table: "ClassSubject",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_subjectId",
                table: "StudentSubject",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyHistory_documentId",
                table: "StudyHistory",
                column: "documentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTime_subjectId",
                table: "StudyTime",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClass_teacherId",
                table: "TeacherClass",
                column: "teacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSubject");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "StudyHistory");

            migrationBuilder.DropTable(
                name: "StudyTime");

            migrationBuilder.DropTable(
                name: "TeacherClass");
        }
    }
}
