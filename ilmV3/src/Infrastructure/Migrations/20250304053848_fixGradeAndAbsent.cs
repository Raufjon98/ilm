using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ilmV3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixGradeAndAbsent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Absent",
                table: "Absents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Absent",
                table: "Absents");
        }
    }
}
