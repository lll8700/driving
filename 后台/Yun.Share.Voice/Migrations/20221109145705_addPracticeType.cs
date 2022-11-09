using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yun.Share.Voice.Migrations
{
    public partial class addPracticeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PracticeTypeId",
                table: "Practices",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "PracticeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletedTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_PracticeTypeId",
                table: "Practices",
                column: "PracticeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_PracticeTypes_PracticeTypeId",
                table: "Practices",
                column: "PracticeTypeId",
                principalTable: "PracticeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_PracticeTypes_PracticeTypeId",
                table: "Practices");

            migrationBuilder.DropTable(
                name: "PracticeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Practices_PracticeTypeId",
                table: "Practices");

            migrationBuilder.DropColumn(
                name: "PracticeTypeId",
                table: "Practices");
        }
    }
}
