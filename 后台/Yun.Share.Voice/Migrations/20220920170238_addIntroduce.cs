using Microsoft.EntityFrameworkCore.Migrations;

namespace Yun.Share.Voice.Migrations
{
    public partial class addIntroduce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Introduce",
                table: "Practices",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Introduce",
                table: "Practices");
        }
    }
}
