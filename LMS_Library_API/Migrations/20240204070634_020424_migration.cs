using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class _020424_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<bool>(type: "bit", nullable: false),
                    submissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    lessonId = table.Column<int>(type: "int", nullable: false),
                    censorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_User_censorId",
                        column: x => x.censorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LessonQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    likesCounter = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isTeacher = table.Column<bool>(type: "bit", nullable: false),
                    lessonId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonQuestion_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonQuestion_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LessonAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    likesCounter = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isTeacher = table.Column<bool>(type: "bit", nullable: false),
                    lessonQuestionId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonAnswer_LessonQuestion_lessonQuestionId",
                        column: x => x.lessonQuestionId,
                        principalTable: "LessonQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonAnswer_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_censorId",
                table: "Document",
                column: "censorId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_lessonId",
                table: "Document",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAnswer_lessonQuestionId",
                table: "LessonAnswer",
                column: "lessonQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAnswer_userId",
                table: "LessonAnswer",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuestion_lessonId",
                table: "LessonQuestion",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuestion_userId",
                table: "LessonQuestion",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "LessonAnswer");

            migrationBuilder.DropTable(
                name: "LessonQuestion");
        }
    }
}
