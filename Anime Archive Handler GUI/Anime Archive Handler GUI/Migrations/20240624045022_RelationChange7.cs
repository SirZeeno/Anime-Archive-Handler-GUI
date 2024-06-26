using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleEntries",
                table: "TitleEntries",
                column: "Type");
        }
    }
}
