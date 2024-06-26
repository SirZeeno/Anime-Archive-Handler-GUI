using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_TimePeriodDto_AiredId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeTrailers_Images_ImageId",
                table: "AnimeTrailers");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_Images_JPGId",
                table: "ImagesSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_Images_WebPId",
                table: "ImagesSets");

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

            migrationBuilder.DropForeignKey(
                name: "FK_TitleEntries_Animes_AnimeDtoMalId",
                table: "TitleEntries");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "TitleSynonym");

            migrationBuilder.DropIndex(
                name: "IX_TitleEntries_AnimeDtoMalId",
                table: "TitleEntries");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_DemographicAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_GenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_LicensorAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ProducerAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_StudioAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_ThemeAnimeId",
                table: "MalUrls");

            migrationBuilder.DropIndex(
                name: "IX_Animes_AiredId",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "AnimeDtoMalId",
                table: "TitleEntries");

            migrationBuilder.DropColumn(
                name: "DemographicAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ExplicitGenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "GenreAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "LicensorAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ProducerAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "StudioAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "ThemeAnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "AiredId",
                table: "Animes");

            migrationBuilder.AddColumn<long>(
                name: "AnimeId",
                table: "TitleEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AnimeId",
                table: "TimePeriodDto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MalUrls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ImageDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitleSynonyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Synonym = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleSynonyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleSynonyms_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntries_AnimeId",
                table: "TitleEntries",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimePeriodDto_AnimeId",
                table: "TimePeriodDto",
                column: "AnimeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeId",
                table: "MalUrls",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleSynonyms_AnimeId",
                table: "TitleSynonyms",
                column: "AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeTrailers_ImageDto_ImageId",
                table: "AnimeTrailers",
                column: "ImageId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_ImageDto_JPGId",
                table: "ImagesSets",
                column: "JPGId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_ImageDto_WebPId",
                table: "ImagesSets",
                column: "WebPId",
                principalTable: "ImageDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeId",
                table: "MalUrls",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimePeriodDto_Animes_AnimeId",
                table: "TimePeriodDto",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleEntries_Animes_AnimeId",
                table: "TitleEntries",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeTrailers_ImageDto_ImageId",
                table: "AnimeTrailers");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_ImageDto_JPGId",
                table: "ImagesSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagesSets_ImageDto_WebPId",
                table: "ImagesSets");

            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeId",
                table: "MalUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_TimePeriodDto_Animes_AnimeId",
                table: "TimePeriodDto");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleEntries_Animes_AnimeId",
                table: "TitleEntries");

            migrationBuilder.DropTable(
                name: "ImageDto");

            migrationBuilder.DropTable(
                name: "TitleSynonyms");

            migrationBuilder.DropIndex(
                name: "IX_TitleEntries_AnimeId",
                table: "TitleEntries");

            migrationBuilder.DropIndex(
                name: "IX_TimePeriodDto_AnimeId",
                table: "TimePeriodDto");

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_AnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "TitleEntries");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "TimePeriodDto");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "MalUrls");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MalUrls");

            migrationBuilder.AddColumn<long>(
                name: "AnimeDtoMalId",
                table: "TitleEntries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DemographicAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExplicitGenreAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GenreAnimeId",
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
                name: "StudioAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThemeAnimeId",
                table: "MalUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AiredId",
                table: "Animes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitleSynonym",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false),
                    Synonym = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleSynonym", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitleSynonym_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntries_AnimeDtoMalId",
                table: "TitleEntries",
                column: "AnimeDtoMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_DemographicAnimeId",
                table: "MalUrls",
                column: "DemographicAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ExplicitGenreAnimeId",
                table: "MalUrls",
                column: "ExplicitGenreAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_GenreAnimeId",
                table: "MalUrls",
                column: "GenreAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_LicensorAnimeId",
                table: "MalUrls",
                column: "LicensorAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ProducerAnimeId",
                table: "MalUrls",
                column: "ProducerAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_StudioAnimeId",
                table: "MalUrls",
                column: "StudioAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_ThemeAnimeId",
                table: "MalUrls",
                column: "ThemeAnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_AiredId",
                table: "Animes",
                column: "AiredId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleSynonym_AnimeId",
                table: "TitleSynonym",
                column: "AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_TimePeriodDto_AiredId",
                table: "Animes",
                column: "AiredId",
                principalTable: "TimePeriodDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeTrailers_Images_ImageId",
                table: "AnimeTrailers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_Images_JPGId",
                table: "ImagesSets",
                column: "JPGId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesSets_Images_WebPId",
                table: "ImagesSets",
                column: "WebPId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TitleEntries_Animes_AnimeDtoMalId",
                table: "TitleEntries",
                column: "AnimeDtoMalId",
                principalTable: "Animes",
                principalColumn: "MalId");
        }
    }
}
