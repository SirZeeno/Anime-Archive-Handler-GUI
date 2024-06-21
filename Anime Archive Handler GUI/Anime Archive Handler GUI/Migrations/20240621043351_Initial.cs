using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    MalId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeSeason = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimePart = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Episodes = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Airing = table.Column<bool>(type: "INTEGER", nullable: false),
                    Duration = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<string>(type: "TEXT", nullable: false),
                    Score = table.Column<double>(type: "REAL", nullable: true),
                    ScoredBy = table.Column<int>(type: "INTEGER", nullable: true),
                    Rank = table.Column<int>(type: "INTEGER", nullable: true),
                    Popularity = table.Column<int>(type: "INTEGER", nullable: true),
                    Members = table.Column<int>(type: "INTEGER", nullable: true),
                    Favorites = table.Column<int>(type: "INTEGER", nullable: true),
                    Synopsis = table.Column<string>(type: "TEXT", nullable: false),
                    Background = table.Column<string>(type: "TEXT", nullable: false),
                    Season = table.Column<int>(type: "INTEGER", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: true),
                    Approved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.MalId);
                });

            migrationBuilder.CreateTable(
                name: "Broadcasts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<string>(type: "TEXT", nullable: false),
                    Timezone = table.Column<string>(type: "TEXT", nullable: false),
                    String = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broadcasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Broadcasts_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageSets_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MalUrls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MalId = table.Column<long>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<DateTime>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimePeriods_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Titles_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    YoutubeId = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    EmbedUrl = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trailers_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeDemographics",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDemographics", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeDemographics_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeDemographics_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeExplicitGenres",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeExplicitGenres", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeExplicitGenres_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeExplicitGenres_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeGenres",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeGenres", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeGenres_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeGenres_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeLicensors",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeLicensors", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeLicensors_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeLicensors_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeProducers",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeProducers", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeProducers_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeProducers_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeStudios",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeStudios", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeStudios_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeStudios_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeThemes",
                columns: table => new
                {
                    AnimeDtoId = table.Column<long>(type: "INTEGER", nullable: false),
                    MalUrlId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeThemes", x => new { x.AnimeDtoId, x.MalUrlId });
                    table.ForeignKey(
                        name: "FK_AnimeThemes_Animes_AnimeDtoId",
                        column: x => x.AnimeDtoId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeThemes_MalUrls_MalUrlId",
                        column: x => x.MalUrlId,
                        principalTable: "MalUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    SmallImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    MediumImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    LargeImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    MaximumImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<long>(type: "INTEGER", nullable: true),
                    ImagesSetJpgId = table.Column<long>(type: "INTEGER", nullable: true),
                    ImagesSetWebPId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ImageSets_ImagesSetJpgId",
                        column: x => x.ImagesSetJpgId,
                        principalTable: "ImageSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_ImageSets_ImagesSetWebPId",
                        column: x => x.ImagesSetWebPId,
                        principalTable: "ImageSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Trailers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Trailers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeDemographics_MalUrlId",
                table: "AnimeDemographics",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeExplicitGenres_MalUrlId",
                table: "AnimeExplicitGenres",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGenres_MalUrlId",
                table: "AnimeGenres",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeLicensors_MalUrlId",
                table: "AnimeLicensors",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeProducers_MalUrlId",
                table: "AnimeProducers",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeStudios_MalUrlId",
                table: "AnimeStudios",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeThemes_MalUrlId",
                table: "AnimeThemes",
                column: "MalUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcasts_AnimeDtoId",
                table: "Broadcasts",
                column: "AnimeDtoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImagesSetJpgId",
                table: "Images",
                column: "ImagesSetJpgId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImagesSetWebPId",
                table: "Images",
                column: "ImagesSetWebPId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ParentId",
                table: "Images",
                column: "ParentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageSets_AnimeDtoId",
                table: "ImageSets",
                column: "AnimeDtoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoId",
                table: "MalUrls",
                column: "AnimeDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_TimePeriods_AnimeDtoId",
                table: "TimePeriods",
                column: "AnimeDtoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_AnimeDtoId",
                table: "Titles",
                column: "AnimeDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_AnimeDtoId",
                table: "Trailers",
                column: "AnimeDtoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeDemographics");

            migrationBuilder.DropTable(
                name: "AnimeExplicitGenres");

            migrationBuilder.DropTable(
                name: "AnimeGenres");

            migrationBuilder.DropTable(
                name: "AnimeLicensors");

            migrationBuilder.DropTable(
                name: "AnimeProducers");

            migrationBuilder.DropTable(
                name: "AnimeStudios");

            migrationBuilder.DropTable(
                name: "AnimeThemes");

            migrationBuilder.DropTable(
                name: "Broadcasts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "MalUrls");

            migrationBuilder.DropTable(
                name: "ImageSets");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "Animes");
        }
    }
}
