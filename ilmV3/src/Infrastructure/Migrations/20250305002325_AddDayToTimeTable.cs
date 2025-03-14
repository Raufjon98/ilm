using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ilmV3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDayToTimeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "TimeTables",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "TimeTables",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "TimeTables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "TimeTables");
        }
    }
}
