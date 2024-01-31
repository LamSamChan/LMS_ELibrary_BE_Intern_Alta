using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class firstDB310124_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Help",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Help", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Help_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DateChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateFile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QnALikes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionsLikedID = table.Column<string>(type: "varchar", nullable: false),
                    AnswersLikedID = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QnALikes", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_QnALikes_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Help_UserId",
                table: "Help",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateFile_UserId",
                table: "PrivateFile",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Help");

            migrationBuilder.DropTable(
                name: "PrivateFile");

            migrationBuilder.DropTable(
                name: "QnALikes");
        }
    }
}
