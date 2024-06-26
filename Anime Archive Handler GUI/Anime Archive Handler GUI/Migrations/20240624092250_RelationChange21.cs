using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "DemographicsDto_ExplicitGenreAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "DemographicsDto_ExplicitGenreAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
