using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId1",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId2",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId3",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId4",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId5",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId6",
                table: "MalUrls");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId6",
                table: "MalUrls",
                newName: "ThemeAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId5",
                table: "MalUrls",
                newName: "StudioAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId4",
                table: "MalUrls",
                newName: "ProducerAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId3",
                table: "MalUrls",
                newName: "LicensorAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId2",
                table: "MalUrls",
                newName: "GenreAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId1",
                table: "MalUrls",
                newName: "ExplicitGenreAnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeDtoMalId",
                table: "MalUrls",
                newName: "DemographicAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId6",
                table: "MalUrls",
                newName: "IX_MalUrls_ThemeAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId5",
                table: "MalUrls",
                newName: "IX_MalUrls_StudioAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId4",
                table: "MalUrls",
                newName: "IX_MalUrls_ProducerAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId3",
                table: "MalUrls",
                newName: "IX_MalUrls_LicensorAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId2",
                table: "MalUrls",
                newName: "IX_MalUrls_GenreAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId1",
                table: "MalUrls",
                newName: "IX_MalUrls_ExplicitGenreAnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_AnimeDtoMalId",
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
                newName: "AnimeDtoMalId6");

            migrationBuilder.RenameColumn(
                name: "StudioAnimeId",
                table: "MalUrls",
                newName: "AnimeDtoMalId5");

            migrationBuilder.RenameColumn(
                name: "ProducerAnimeId",
                table: "MalUrls",
                newName: "AnimeDtoMalId4");

            migrationBuilder.RenameColumn(
                name: "LicensorAnimeId",
                table: "MalUrls",
                newName: "AnimeDtoMalId3");

            migrationBuilder.RenameColumn(
                name: "GenreAnimeId",
                table: "MalUrls",
                newName: "AnimeDtoMalId2");

            migrationBuilder.RenameColumn(
                name: "ExplicitGenreAnimeId",
                table: "MalUrls",
                newName: "AnimeDtoMalId1");

            migrationBuilder.RenameColumn(
                name: "DemographicAnimeId",
                table: "MalUrls",
                newName: "AnimeDtoMalId");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ThemeAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId6");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_StudioAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId5");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ProducerAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId4");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_LicensorAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId3");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_GenreAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId2");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_ExplicitGenreAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId1");

            migrationBuilder.RenameIndex(
                name: "IX_MalUrls_DemographicAnimeId",
                table: "MalUrls",
                newName: "IX_MalUrls_AnimeDtoMalId");

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId",
                table: "MalUrls",
                column: "AnimeDtoMalId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId1",
                table: "MalUrls",
                column: "AnimeDtoMalId1",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId2",
                table: "MalUrls",
                column: "AnimeDtoMalId2",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId3",
                table: "MalUrls",
                column: "AnimeDtoMalId3",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId4",
                table: "MalUrls",
                column: "AnimeDtoMalId4",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId5",
                table: "MalUrls",
                column: "AnimeDtoMalId5",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeDtoMalId6",
                table: "MalUrls",
                column: "AnimeDtoMalId6",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
