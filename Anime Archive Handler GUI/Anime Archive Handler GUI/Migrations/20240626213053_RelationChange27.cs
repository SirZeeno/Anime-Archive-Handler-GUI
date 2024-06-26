using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_LicensorAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ProducerAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ThemeAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_LicensorAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ProducerAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ThemeAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "LicensorAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ProducerAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ThemeAnimeId",
                table: "MalUrls");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeId1",
                table: "MalUrls",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeId1",
                table: "MalUrls");

            migrationBuilder.AddColumn<long>(
                name: "DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExplicitGenreAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LicensorAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProducerAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThemeAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "DemographicsDto_ExplicitGenreAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "ExplicitGenreAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_LicensorAnimeId",
                table: "MalUrls",
                column: "LicensorAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ProducerAnimeId",
                table: "MalUrls",
                column: "ProducerAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ThemeAnimeId",
                table: "MalUrls",
                column: "ThemeAnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "DemographicsDto_ExplicitGenreAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "ExplicitGenreAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_LicensorAnimeId",
                table: "MalUrls",
                column: "LicensorAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ProducerAnimeId",
                table: "MalUrls",
                column: "ProducerAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ThemeAnimeId",
                table: "MalUrls",
                column: "ThemeAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
