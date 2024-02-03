using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class _020324Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Part",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    dateSubmited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isHidden = table.Column<bool>(type: "bit", nullable: false),
                    numericalOrder = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    censorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    teacherCreatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subjectId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Part", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Part_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Part_User_censorId",
                        column: x => x.censorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Part_User_teacherCreatedId",
                        column: x => x.teacherCreatedId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    dateSubmited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isHidden = table.Column<bool>(type: "bit", nullable: false),
                    numericalOrder = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    partId = table.Column<int>(type: "int", nullable: false),
                    censorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    teacherCreatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Part_partId",
                        column: x => x.partId,
                        principalTable: "Part",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lesson_User_censorId",
                        column: x => x.censorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lesson_User_teacherCreatedId",
                        column: x => x.teacherCreatedId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_censorId",
                table: "Lesson",
                column: "censorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_partId",
                table: "Lesson",
                column: "partId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_teacherCreatedId",
                table: "Lesson",
                column: "teacherCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Part_censorId",
                table: "Part",
                column: "censorId");

            migrationBuilder.CreateIndex(
                name: "IX_Part_subjectId",
                table: "Part",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Part_teacherCreatedId",
                table: "Part",
                column: "teacherCreatedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Part");
        }
    }
}
