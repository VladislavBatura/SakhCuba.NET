using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhCuba.Migrations
{
    public partial class DecisionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiscordName",
                table: "Applications",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Decision",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decision",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "DiscordName",
                table: "Applications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
