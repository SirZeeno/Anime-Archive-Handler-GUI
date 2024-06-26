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
                name: "AnimeBroadcasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<string>(type: "TEXT", nullable: false),
                    Timezone = table.Column<string>(type: "TEXT", nullable: false),
                    String = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeBroadcasts", x => x.Id);
                });

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
                name: "TimePeriod",
                columns: table => new
                {
                    From = table.Column<DateTime>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TimePeriodDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    From = table.Column<DateTime>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriodDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeTrailers",
                columns: table => new
                {
                    YoutubeId = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    EmbedUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ImageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeTrailers", x => x.YoutubeId);
                    table.ForeignKey(
                        name: "FK_AnimeTrailers_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagesSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JPGId = table.Column<int>(type: "INTEGER", nullable: false),
                    WebPId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagesSets_Images_JPGId",
                        column: x => x.JPGId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagesSets_Images_WebPId",
                        column: x => x.WebPId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    MalId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ImagesId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrailerYoutubeId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    TitleEnglish = table.Column<string>(type: "TEXT", nullable: false),
                    TitleJapanese = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Episodes = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Airing = table.Column<bool>(type: "INTEGER", nullable: false),
                    AiredId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    BroadcastId = table.Column<int>(type: "INTEGER", nullable: false),
                    Approved = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.MalId);
                    table.ForeignKey(
                        name: "FK_Animes_AnimeBroadcasts_BroadcastId",
                        column: x => x.BroadcastId,
                        principalTable: "AnimeBroadcasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animes_AnimeTrailers_TrailerYoutubeId",
                        column: x => x.TrailerYoutubeId,
                        principalTable: "AnimeTrailers",
                        principalColumn: "YoutubeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animes_ImagesSets_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "ImagesSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Animes_TimePeriodDto_AiredId",
                        column: x => x.AiredId,
                        principalTable: "TimePeriodDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MalUrls",
                columns: table => new
                {
                    MalId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: true),
                    AnimeDtoMalId1 = table.Column<long>(type: "INTEGER", nullable: true),
                    AnimeDtoMalId2 = table.Column<long>(type: "INTEGER", nullable: true),
                    AnimeDtoMalId3 = table.Column<long>(type: "INTEGER", nullable: true),
                    AnimeDtoMalId4 = table.Column<long>(type: "INTEGER", nullable: true),
                    AnimeDtoMalId5 = table.Column<long>(type: "INTEGER", nullable: true),
                    AnimeDtoMalId6 = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalUrls", x => x.MalId);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId1",
                        column: x => x.AnimeDtoMalId1,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId2",
                        column: x => x.AnimeDtoMalId2,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId3",
                        column: x => x.AnimeDtoMalId3,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId4",
                        column: x => x.AnimeDtoMalId4,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId5",
                        column: x => x.AnimeDtoMalId5,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalUrls_Animes_AnimeDtoMalId6",
                        column: x => x.AnimeDtoMalId6,
                        principalTable: "Animes",
                        principalColumn: "MalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleEntries",
                columns: table => new
                {
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeDtoMalId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleEntries", x => x.Type);
                    table.ForeignKey(
                        name: "FK_TitleEntries_Animes_AnimeDtoMalId",
                        column: x => x.AnimeDtoMalId,
                        principalTable: "Animes",
                        principalColumn: "MalId");
                });

            migrationBuilder.CreateTable(
                name: "TitleSynonym",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Synonym = table.Column<string>(type: "TEXT", nullable: false),
                    AnimeId = table.Column<long>(type: "INTEGER", nullable: false)
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
                name: "IX_Animes_AiredId",
                table: "Animes",
                column: "AiredId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_BroadcastId",
                table: "Animes",
                column: "BroadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_ImagesId",
                table: "Animes",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Animes_TrailerYoutubeId",
                table: "Animes",
                column: "TrailerYoutubeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTrailers_ImageId",
                table: "AnimeTrailers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesSets_JPGId",
                table: "ImagesSets",
                column: "JPGId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesSets_WebPId",
                table: "ImagesSets",
                column: "WebPId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId",
                table: "MalUrls",
                column: "AnimeDtoMalId");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId1",
                table: "MalUrls",
                column: "AnimeDtoMalId1");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId2",
                table: "MalUrls",
                column: "AnimeDtoMalId2");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId3",
                table: "MalUrls",
                column: "AnimeDtoMalId3");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId4",
                table: "MalUrls",
                column: "AnimeDtoMalId4");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId5",
                table: "MalUrls",
                column: "AnimeDtoMalId5");

            migrationBuilder.CreateIndex(
                name: "IX_MalUrls_AnimeDtoMalId6",
                table: "MalUrls",
                column: "AnimeDtoMalId6");

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntries_AnimeDtoMalId",
                table: "TitleEntries",
                column: "AnimeDtoMalId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleSynonym_AnimeId",
                table: "TitleSynonym",
                column: "AnimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MalUrls");

            migrationBuilder.DropTable(
                name: "TimePeriod");

            migrationBuilder.DropTable(
                name: "TitleEntries");

            migrationBuilder.DropTable(
                name: "TitleSynonym");

            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "AnimeBroadcasts");

            migrationBuilder.DropTable(
                name: "AnimeTrailers");

            migrationBuilder.DropTable(
                name: "ImagesSets");

            migrationBuilder.DropTable(
                name: "TimePeriodDto");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
