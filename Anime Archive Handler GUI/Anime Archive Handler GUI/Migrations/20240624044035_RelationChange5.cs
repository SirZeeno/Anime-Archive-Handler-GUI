using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class RelationChange5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MalUrls_Animes_AnimeId",
                table: "MalUrls");

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

            migrationBuilder.DropIndex(
                name: "IX_MalUrls_AnimeId",
                table: "MalUrls");

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

            migrationBuilder.AddColumn<long>(
                name: "MalUrlDtoMalId",
                table: "Animes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimeDtoDemographicsDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    DemographicsMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoDemographicsDto", x => new { x.AnimeDtoMalId, x.DemographicsMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoDemographicsDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoDemographicsDto_MalUrls_DemographicsMalId",
                        column: x => x.DemographicsMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDtoExplicitGenresDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    ExplicitGenresMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoExplicitGenresDto", x => new { x.AnimeDtoMalId, x.ExplicitGenresMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoExplicitGenresDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoExplicitGenresDto_MalUrls_ExplicitGenresMalId",
                        column: x => x.ExplicitGenresMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDtoGenresDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    GenresMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoGenresDto", x => new { x.AnimeDtoMalId, x.GenresMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoGenresDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoGenresDto_MalUrls_GenresMalId",
                        column: x => x.GenresMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDtoLicensorsDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    LicensorsMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoLicensorsDto", x => new { x.AnimeDtoMalId, x.LicensorsMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoLicensorsDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoLicensorsDto_MalUrls_LicensorsMalId",
                        column: x => x.LicensorsMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDtoProducersDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    ProducersMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoProducersDto", x => new { x.AnimeDtoMalId, x.ProducersMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoProducersDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoProducersDto_MalUrls_ProducersMalId",
                        column: x => x.ProducersMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDtoStudiosDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    StudiosMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoStudiosDto", x => new { x.AnimeDtoMalId, x.StudiosMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoStudiosDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoStudiosDto_MalUrls_StudiosMalId",
                        column: x => x.StudiosMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDtoThemesDto",
                columns: table => new
                {
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: false),
                    ThemesMalId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDtoThemesDto", x => new { x.AnimeDtoMalId, x.ThemesMalId });
                    table.ForeignKey(
                        name: "FK_AnimeDtoThemesDto_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDtoThemesDto_MalUrls_ThemesMalId",
                        column: x => x.ThemesMalId,
                        principalTable: "MalUrls",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animes_MalUrlDtoMalId",
                table: "Animes",
                column: "MalUrlDtoMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoDemographicsDto_DemographicsMalId",
                table: "AnimeDtoDemographicsDto",
                column: "DemographicsMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoExplicitGenresDto_ExplicitGenresMalId",
                table: "AnimeDtoExplicitGenresDto",
                column: "ExplicitGenresMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoGenresDto_GenresMalId",
                table: "AnimeDtoGenresDto",
                column: "GenresMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoLicensorsDto_LicensorsMalId",
                table: "AnimeDtoLicensorsDto",
                column: "LicensorsMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoProducersDto_ProducersMalId",
                table: "AnimeDtoProducersDto",
                column: "ProducersMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoStudiosDto_StudiosMalId",
                table: "AnimeDtoStudiosDto",
                column: "StudiosMalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDtoThemesDto_ThemesMalId",
                table: "AnimeDtoThemesDto",
                column: "ThemesMalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_MalUrls_MalUrlDtoMalId",
                table: "Animes",
                column: "MalUrlDtoMalId",
                principalTable: "MalUrls",
                principalColumn: "MalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_MalUrls_MalUrlDtoMalId",
                table: "Animes");

            migrationBuilder.DropTable(
                name: "AnimeDtoDemographicsDto");

            migrationBuilder.DropTable(
                name: "AnimeDtoExplicitGenresDto");

            migrationBuilder.DropTable(
                name: "AnimeDtoGenresDto");

            migrationBuilder.DropTable(
                name: "AnimeDtoLicensorsDto");

            migrationBuilder.DropTable(
                name: "AnimeDtoProducersDto");

            migrationBuilder.DropTable(
                name: "AnimeDtoStudiosDto");

            migrationBuilder.DropTable(
                name: "AnimeDtoThemesDto");

            migrationBuilder.DropIndex(
                name: "IX_Animes_MalUrlDtoMalId",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "MalUrlDtoMalId",
                table: "Animes");

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

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeId",
                table: "MalUrls",
                column: "AnimeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MalUrls_Animes_AnimeId",
                table: "MalUrls",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "MalId",
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
        }
    }
}
