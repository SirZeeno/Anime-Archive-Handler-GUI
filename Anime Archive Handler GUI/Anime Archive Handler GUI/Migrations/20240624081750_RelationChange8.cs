using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TitleEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<long>(
                name: "AnimeMalId",
                table: "TitleEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntries_AnimeMalId",
                table: "TitleEntries",
                column: "AnimeMalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TitleEntries_Animes_AnimeMalId",
                table: "TitleEntries",
                column: "AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitleEntries_Animes_AnimeMalId",
                table: "TitleEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries");

            migrationBuilder.DropIndex(
                name: "IX_TitleEntries_AnimeMalId",
                table: "TitleEntries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TitleEntries");

            migrationBuilder.DropColumn(
                name: "AnimeMalId",
                table: "TitleEntries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries",
                column: "Title");
        }
    }
}
