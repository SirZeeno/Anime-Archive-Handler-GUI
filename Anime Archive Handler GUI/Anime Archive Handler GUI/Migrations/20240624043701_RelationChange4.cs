using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_GenresDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ProducersDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_StudiosDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ThemesDto_AnimeMalId",
                table: "MalUrls");

            migrationBuilder.RenameColumn(
                name: "ThemesDto_AnimeMalId",
                table: "MalUrls",
                newName: "ThemeAnimeId");

            migrationBuilder.RenameColumn(
                name: "StudiosDto_AnimeMalId",
                table: "MalUrls",
                newName: "StudioAnimeId");

            migrationBuilder.RenameColumn(
                name: "ProducersDto_AnimeMalId",
                table: "MalUrls",
                newName: "ProducerAnimeId");

            migrationBuilder.RenameColumn(
                name: "GenresDto_AnimeMalId",
                table: "MalUrls",
                newName: "LicensorAnimeId");

            migrationBuilder.RenameColumn(
                name: "ExplicitGenresDto_AnimeMalId",
                table: "MalUrls",
                newName: "GenreAnimeId");

            migrationBuilder.RenameColumn(
                name: "DemographicsDto_AnimeMalId",
                table: "MalUrls",
                newName: "ExplicitGenreAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeMalId",
                table: "MalUrls",
                newName: "DemographicAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ThemesDto_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_ThemeAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_StudiosDto_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_StudioAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ProducersDto_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_ProducerAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_GenresDto_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_LicensorAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_GenreAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_DemographicsDto_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_ExplicitGenreAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeMalId",
                table: "MalUrls",
                newName: "IX_MalUrls_DemographicAnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_DemographicAnimeId",
                table: "MalUrls",
                column: "DemographicAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "ExplicitGenreAnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_GenreAnimeId",
                table: "MalUrls",
                column: "GenreAnimeId",
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
                name: "FK_MalUrls_Animes_StudioAnimeId",
                table: "MalUrls",
                column: "StudioAnimeId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_DemographicAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_GenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_LicensorAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ProducerAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_StudioAnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_ThemeAnimeId",
                table: "MalUrls");

            migrationBuilder.RenameColumn(
                name: "ThemeAnimeId",
                table: "MalUrls",
                newName: "ThemesDto_AnimeMalId");

            migrationBuilder.RenameColumn(
                name: "StudioAnimeId",
                table: "MalUrls",
                newName: "StudiosDto_AnimeMalId");

            migrationBuilder.RenameColumn(
                name: "ProducerAnimeId",
                table: "MalUrls",
                newName: "ProducersDto_AnimeMalId");

            migrationBuilder.RenameColumn(
                name: "LicensorAnimeId",
                table: "MalUrls",
                newName: "GenresDto_AnimeMalId");

            migrationBuilder.RenameColumn(
                name: "GenreAnimeId",
                table: "MalUrls",
                newName: "ExplicitGenresDto_AnimeMalId");

            migrationBuilder.RenameColumn(
                name: "ExplicitGenreAnimeId",
                table: "MalUrls",
                newName: "DemographicsDto_AnimeMalId");

            migrationBuilder.RenameColumn(
                name: "DemographicAnimeId",
                table: "MalUrls",
                newName: "AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ThemeAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_ThemesDto_AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_StudioAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_StudiosDto_AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ProducerAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_ProducersDto_AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_LicensorAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_GenresDto_AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_GenreAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_ExplicitGenresDto_AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ExplicitGenreAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_DemographicsDto_AnimeMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_DemographicAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeMalId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeMalId",
                table: "MalUrls",
                column: "AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_DemographicsDto_AnimeMalId",
                table: "MalUrls",
                column: "DemographicsDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ExplicitGenresDto_AnimeMalId",
                table: "MalUrls",
                column: "ExplicitGenresDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_GenresDto_AnimeMalId",
                table: "MalUrls",
                column: "GenresDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ProducersDto_AnimeMalId",
                table: "MalUrls",
                column: "ProducersDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_StudiosDto_AnimeMalId",
                table: "MalUrls",
                column: "StudiosDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_ThemesDto_AnimeMalId",
                table: "MalUrls",
                column: "ThemesDto_AnimeMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
