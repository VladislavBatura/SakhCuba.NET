using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhCuba.Migrations
{
    public partial class newsfork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_NewsId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "News",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsId",
                table: "Images",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_NewsId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "News");

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsId",
                table: "Images",
                column: "NewsId",
                unique: true);
        }
    }
}
