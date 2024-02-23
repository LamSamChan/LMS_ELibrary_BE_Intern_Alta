using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Part_User_censorId",
                table: "Part");

            migrationBuilder.AlterColumn<Guid>(
                name: "censorId",
                table: "Part",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Exam",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_DepartmentId",
                table: "Exam",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Department_DepartmentId",
                table: "Exam",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Part_User_censorId",
                table: "Part",
                column: "censorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Department_DepartmentId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Part_User_censorId",
                table: "Part");

            migrationBuilder.DropIndex(
                name: "IX_Exam_DepartmentId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Exam");

            migrationBuilder.AlterColumn<Guid>(
                name: "censorId",
                table: "Part",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Part_User_censorId",
                table: "Part",
                column: "censorId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
