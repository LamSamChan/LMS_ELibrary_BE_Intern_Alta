using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateUserDocumentUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "Part",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AddColumn<Guid>(
                name: "teacherCreatedId",
                table: "Document",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Document_teacherCreatedId",
                table: "Document",
                column: "teacherCreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_User_teacherCreatedId",
                table: "Document",
                column: "teacherCreatedId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_User_teacherCreatedId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_teacherCreatedId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "teacherCreatedId",
                table: "Document");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "Part",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);
        }
    }
}
