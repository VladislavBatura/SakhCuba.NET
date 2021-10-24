using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhCuba.Migrations
{
    public partial class imagedbupload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "News",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "News",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
