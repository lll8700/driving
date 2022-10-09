using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yun.Share.Voice.Migrations
{
    public partial class adderrorPracticeLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ErrorPracticeLoges",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ErrorPracticeIds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ErrorPracticeLogId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OptionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                    table.PrimaryKey("PK_ErrorPracticeIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorPracticeIds_ErrorPracticeLoges_ErrorPracticeLogId",
                        column: x => x.ErrorPracticeLogId,
                        principalTable: "ErrorPracticeLoges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErrorPracticeIds_Optiones_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Optiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorPracticeIds_ErrorPracticeLogId",
                table: "ErrorPracticeIds",
                column: "ErrorPracticeLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorPracticeIds_OptionId",
                table: "ErrorPracticeIds",
                column: "OptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorPracticeIds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ErrorPracticeLoges");
        }
    }
}
