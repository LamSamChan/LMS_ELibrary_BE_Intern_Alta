using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_Library_API.Migrations
{
    public partial class updateModelStudentNotificationFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "featureType",
                table: "StudentNotificationFeatures",
                newName: "FeatureType");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "StudentNotificationFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "StudentNotificationFeatures");

            migrationBuilder.RenameColumn(
                name: "FeatureType",
                table: "StudentNotificationFeatures",
                newName: "featureType");
        }
    }
}
