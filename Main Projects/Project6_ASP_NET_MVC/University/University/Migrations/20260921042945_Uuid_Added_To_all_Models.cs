using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    /// <inheritdoc />
    public partial class Uuid_Added_To_all_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "WaitlistEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "DegreeRequirements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "DegreePrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "CourseSections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "CoursePrerequesites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "WaitlistEntries");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "DegreeRequirements");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "DegreePrograms");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "CoursePrerequesites");
        }
    }
}
