using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhCuba.Migrations
{
    public partial class IpAndDateAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Applications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "Applications");
        }
    }
}
