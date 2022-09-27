using Microsoft.EntityFrameworkCore.Migrations;

namespace Yun.Share.Voice.Migrations
{
    public partial class addCarTypename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "CarTypes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Subname",
                table: "CarTypes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "CarTypes");

            migrationBuilder.DropColumn(
                name: "Subname",
                table: "CarTypes");
        }
    }
}
