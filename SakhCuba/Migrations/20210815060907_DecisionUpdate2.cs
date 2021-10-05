using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhCuba.Migrations
{
    public partial class DecisionUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decision",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "DecisionId",
                table: "Applications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Decisions",
                columns: table => new
                {
                    DecisionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DecisionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decisions", x => x.DecisionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_DecisionId",
                table: "Applications",
                column: "DecisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Decisions_DecisionId",
                table: "Applications",
                column: "DecisionId",
                principalTable: "Decisions",
                principalColumn: "DecisionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Decisions_DecisionId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "Decisions");

            migrationBuilder.DropIndex(
                name: "IX_Applications_DecisionId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DecisionId",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "Decision",
                table: "Applications",
                nullable: true);
        }
    }
}
