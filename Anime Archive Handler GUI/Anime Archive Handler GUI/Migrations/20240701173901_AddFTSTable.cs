using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class AddFTSTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIRTUAL TABLE IF NOT EXISTS Titles_fts USING fts5(AnimeId UNINDEXED, Title, Type UNINDEXED);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Titles_fts");
        }
    }
}
