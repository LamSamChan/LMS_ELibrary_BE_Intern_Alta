using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion");

            migrationBuilder.DropTable(
                name: "QnALikes");

            migrationBuilder.DropTable(
                name: "StudentQnALikes");

            migrationBuilder.AlterColumn<Guid>(
                name: "studentId",
                table: "LessonQuestion",
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

            migrationBuilder.CreateTable(
                name: "AnswerLike",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LessonAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerLike", x => new { x.UserId, x.LessonId, x.LessonAnswerId });
                    table.ForeignKey(
                        name: "FK_AnswerLike_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerLike_LessonAnswer_LessonAnswerId",
                        column: x => x.LessonAnswerId,
                        principalTable: "LessonAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AnswerLike_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QuestionLike",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LessonQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionLike", x => new { x.UserId, x.LessonId, x.LessonQuestionId });
                    table.ForeignKey(
                        name: "FK_QuestionLike_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuestionLike_LessonQuestion_LessonQuestionId",
                        column: x => x.LessonQuestionId,
                        principalTable: "LessonQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuestionLike_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswerLike",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LessonAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswerLike", x => new { x.StudentId, x.LessonId, x.LessonAnswerId });
                    table.ForeignKey(
                        name: "FK_StudentAnswerLike_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswerLike_LessonAnswer_LessonAnswerId",
                        column: x => x.LessonAnswerId,
                        principalTable: "LessonAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentAnswerLike_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentQuestionLike",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    LessonQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuestionLike", x => new { x.StudentId, x.LessonId, x.LessonQuestionId });
                    table.ForeignKey(
                        name: "FK_StudentQuestionLike_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentQuestionLike_LessonQuestion_LessonQuestionId",
                        column: x => x.LessonQuestionId,
                        principalTable: "LessonQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentQuestionLike_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLike_LessonAnswerId",
                table: "AnswerLike",
                column: "LessonAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLike_LessonId",
                table: "AnswerLike",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionLike_LessonId",
                table: "QuestionLike",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionLike_LessonQuestionId",
                table: "QuestionLike",
                column: "LessonQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerLike_LessonAnswerId",
                table: "StudentAnswerLike",
                column: "LessonAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswerLike_LessonId",
                table: "StudentAnswerLike",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestionLike_LessonId",
                table: "StudentQuestionLike",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuestionLike_LessonQuestionId",
                table: "StudentQuestionLike",
                column: "LessonQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion");

            migrationBuilder.DropTable(
                name: "AnswerLike");

            migrationBuilder.DropTable(
                name: "QuestionLike");

            migrationBuilder.DropTable(
                name: "StudentAnswerLike");

            migrationBuilder.DropTable(
                name: "StudentQuestionLike");

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
                name: "studentId",
                table: "LessonAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "QnALikes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswersLikedID = table.Column<string>(type: "varchar(max)", nullable: false),
                    QuestionsLikedID = table.Column<string>(type: "varchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "StudentQnALikes",
                columns: table => new
                {
                    studentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswersLikedID = table.Column<string>(type: "varchar(max)", nullable: false),
                    QuestionsLikedID = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQnALikes", x => x.studentId);
                    table.ForeignKey(
                        name: "FK_StudentQnALikes_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAnswer_Student_studentId",
                table: "LessonAnswer",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonQuestion_Student_studentId",
                table: "LessonQuestion",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
