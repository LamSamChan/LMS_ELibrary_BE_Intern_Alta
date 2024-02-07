using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class migration_070224 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentAccess",
                columns: table => new
                {
                    documentId = table.Column<int>(type: "int", nullable: false),
                    classId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    isForAllClasses = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAccess", x => x.documentId);
                    table.ForeignKey(
                        name: "FK_DocumentAccess_Class_classId",
                        column: x => x.classId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentAccess_Document_documentId",
                        column: x => x.documentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonAccess",
                columns: table => new
                {
                    lessonId = table.Column<int>(type: "int", nullable: false),
                    classId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    isForAllClasses = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAccess", x => x.lessonId);
                    table.ForeignKey(
                        name: "FK_LessonAccess_Class_classId",
                        column: x => x.classId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonAccess_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAccess_classId",
                table: "DocumentAccess",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAccess_classId",
                table: "LessonAccess",
                column: "classId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentAccess");

            migrationBuilder.DropTable(
                name: "LessonAccess");
        }
    }
}
